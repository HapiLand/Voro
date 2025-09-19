using System;
using UnityEngine;
using UnityEngine.UIElements;

namespace VoroUI {
/// <summary>
///     a float slider control
/// </summary>
public class FloatSliderElement : ControlElement<float> {
    readonly FloatField _field;
    readonly Slider _slider;
    Func<float> _dataGet;
    Action<float> _dataSet;
    float _value; // current value of the slider

    /// <summary>
    ///     element can be set as a logarithmic scale for the slider
    ///     by default the slider for the element is a linear scale
    /// </summary>
    public bool IsLogScale = false;

    public FloatSliderElement() {
        style.flexDirection = FlexDirection.Row; // horizontal row
        // field to display value
        _field = new FloatField();
        FieldContainer.Add(_field);
        _field.RegisterValueChangedCallback(evt => {
            // set the value of the field as the input
            // the user entered a value to the field, so that value is what is written
            SetValueWithoutNotify(evt.newValue);

            // set the slider value by finding where the field input value is in the log range
            var logT = IsLogScale ? SolveLogT(MinValue, MaxValue, evt.newValue) : evt.newValue;
            _slider.SetValueWithoutNotify(logT);
            _dataSet?.Invoke(logT);
        });
        // slider to alter value
        _slider = new Slider();
        ControlContainer.Add(_slider);
        _slider.RegisterValueChangedCallback(evt => {
            // set the value of the slider
            // do not use a log scale as the slider itself uses a linear control range
            SetValueWithoutNotify(evt.newValue);

            // set the field value as logarithmic
            var logValue = IsLogScale ? LogInterpolate(MinValue, MaxValue, evt.newValue) : evt.newValue;
            _field.SetValueWithoutNotify(logValue);

            // output the logarithmic float value to write to the effect data
            _dataSet?.Invoke(evt.newValue);
        });
        return;

        // find value of T, the value that is between a and b
        // eg
        // a=0, b=100, value=80   T=0.8
        float SolveLogT(float a, float b, float value) {
            var logA = (float)Math.Log(a);
            var logB = (float)Math.Log(b);
            var logValue = (float)Math.Log(value);
            return (logValue - logA) / (logB - logA);
        }

        // a lerp function for a log scale
        float LogInterpolate(float a, float b, float t) {
            var logA = (float)Math.Log(a);
            var logB = (float)Math.Log(b);
            var logValue = logA + (logB - logA) * t;
            return (float)Math.Exp(logValue);
        }
    }


    [UxmlAttribute]
    public float Value {
        get => _dataGet?.Invoke() ?? 0f;
        set => SetValueWithoutNotify(value);
    }

    [UxmlAttribute]
    public float MinValue {
        get => _slider?.lowValue ?? 0f;
        set => _slider.lowValue = value;
    }

    [UxmlAttribute]
    public float MaxValue {
        get => _slider?.highValue ?? 0f;
        set => _slider.highValue = value;
    }

    public void BindToData(Func<float> getter, Action<float> setter) {
        _dataGet = getter;
        _dataSet = setter;
        Refresh();
    }

    /// <summary>
    ///     if the data is changed externally, the value shown by the controls must be updated
    /// </summary>
    public void Refresh() {
        if (_dataGet != null) {
            var value = Mathf.Clamp(_dataGet(), _slider.lowValue, _slider.highValue);
            _slider?.SetValueWithoutNotify(value);
            _field?.SetValueWithoutNotify(value);
        }
    }

    void SetValueWithoutNotify(float value) {
        _slider?.SetValueWithoutNotify(value);
        _field?.SetValueWithoutNotify(value);
    }
}
}