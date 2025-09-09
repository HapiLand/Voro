using System;
using UnityEngine;
using UnityEngine.UIElements;
using VoroEditor.Utility;

namespace VoroEditor.Elements {
[UxmlElement]
public partial class FloatSlider : VisualElement {
    readonly FloatField _field;
    readonly Label _label;
    readonly Slider _slider;

    Func<float> _getter;
    Action<float> _setter;
    float _value; // current value of the slider

    public FloatSlider() {
        // instantiate the uxml
        var vt = UIHelper.LoadUxml("FloatSlider");
        vt.CloneTree(this);

        // query each element
        _label = this.Q<Label>("Label");
        _field = this.Q<FloatField>("Field");
        _slider = this.Q<Slider>("Slider");

        // register callbacks
        _slider.RegisterValueChangedCallback(evt => {
            SetValueWithoutNotify(evt.newValue);
            _field.SetValueWithoutNotify(evt.newValue);
            _setter?.Invoke(evt.newValue);
        });
        _field.RegisterValueChangedCallback(evt => {
            SetValueWithoutNotify(evt.newValue);
            _slider.SetValueWithoutNotify(evt.newValue);
            _setter?.Invoke(evt.newValue);
        });
    }

    /// <summary>
    ///     bind the slider to the external data
    /// </summary>
    public void Bind(Func<float> getter, Action<float> setter) {
        _getter = getter;
        _setter = setter;
        Refresh();
    }

    /// <summary>
    ///     refresh the slider whenever the data changes externally
    /// </summary>
    public void Refresh() {
        if (_getter != null) {
            var value = Mathf.Clamp(_getter(), _slider.lowValue, _slider.highValue);
            _slider?.SetValueWithoutNotify(value);
            _field?.SetValueWithoutNotify(value);
        }
    }

    void SetValueWithoutNotify(float value) {
        _slider?.SetValueWithoutNotify(value);
        _field?.SetValueWithoutNotify(value);
    }

    #region UXML Attributes

    [UxmlAttribute]
    public string DisplayName {
        get => _label?.text ?? "";
        set => _label.text = value;
    }

    [UxmlAttribute]
    public float Value {
        get => _getter?.Invoke() ?? 0f;
        set => SetValueWithoutNotify(value);
    }

    [UxmlAttribute]
    public float MinValue {
        get => _slider?.lowValue ?? 0f;
        set => _slider.lowValue = value;
    }

    [UxmlAttribute]
    public float MaxValue {
        get => _slider?.highValue ?? 1f;
        set => _slider.highValue = value;
    }

    [UxmlAttribute] public bool IsLogarithmic { get; set; }
    // ToDo implement logarithmic scale

    #endregion
}
}