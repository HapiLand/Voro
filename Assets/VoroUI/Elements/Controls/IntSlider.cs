using System;
using UnityEngine;
using UnityEngine.UIElements;
using VoroUI.Elements.Base;

namespace VoroUI.Elements.Controls {
/// <summary>
///     a float slider control
/// </summary>
public class IntSlider : Control<int> {
    readonly IntegerField _field;

    /// <summary>
    ///     Slider is not an IntergerSlider, as the slider is just to
    ///     serve as an interpolation between the min and max integer values
    /// </summary>
    readonly Slider _slider;

    Func<int> _dataGet;
    Action<int> _dataSet;

    public IntSlider() {
        style.flexDirection = FlexDirection.Row; // horizontal row

        // field to display value
        _field = new IntegerField();
        FieldContainer.Add(_field);
        _field.RegisterValueChangedCallback(evt => {
            var value = evt.newValue;
            SetValueWithoutNotify(value);
            _slider.SetValueWithoutNotify(value);
            _dataSet?.Invoke(value);
        });

        // slider to alter value
        _slider = new Slider();
        ControlContainer.Add(_slider);
        _slider.RegisterValueChangedCallback(evt => {
            var intValue = Mathf.RoundToInt(evt.newValue);
            SetValueWithoutNotify(intValue);
            _dataSet?.Invoke(intValue);
        });
    }

    [UxmlAttribute]
    public int Value {
        get => _dataGet?.Invoke() ?? 0;
        set => SetValueWithoutNotify(value);
    }

    [UxmlAttribute]
    public int MinValue {
        get => Mathf.RoundToInt(_slider?.lowValue ?? 0);
        set => _slider.lowValue = value;
    }

    [UxmlAttribute]
    public int MaxValue {
        get => Mathf.RoundToInt(_slider?.highValue ?? 0);
        set => _slider.highValue = value;
    }

    public void BindToData(Func<int> getter, Action<int> setter) {
        _dataGet = getter;
        _dataSet = setter;
        Refresh();
    }

    /// <summary>
    ///     if the data is changed externally, the value shown by the controls must be updated
    /// </summary>
    public void Refresh() {
        if (_dataGet != null) {
            var value = Mathf.Clamp(_dataGet(), Mathf.RoundToInt(_slider.lowValue),
                Mathf.RoundToInt(_slider.highValue));
            _slider?.SetValueWithoutNotify(value);
            _field?.SetValueWithoutNotify(value);
        }
    }


    void SetValueWithoutNotify(int value) {
        _field?.SetValueWithoutNotify(value);
        _slider?.SetValueWithoutNotify(value);
    }
}
}