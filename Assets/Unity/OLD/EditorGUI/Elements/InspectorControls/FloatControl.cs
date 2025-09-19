using System;
using EditorGUI.Elements.Internal;
using EditorGUI.Source.Utility;
using UnityEngine;
using UnityEngine.UIElements;

namespace EditorGUI.Elements.InspectorControls {
/// <summary>
///     ControlElements is a generic class so that InspectorElement can be designed to use a range of controls
/// </summary>
[UxmlElement]
public partial class FloatControl : ControlElement<float> {
    readonly FloatField _field;
    readonly Slider _slider;
    Func<float> _getter;
    Action<float> _setter;
    float _value; // current value of the slider

    public FloatControl() {
        _slider = new Slider();
        _controlContainer.Add(_slider);

        _field = new FloatField();
        _controlsField.Add(_field);

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

        AssetHelper.LoadAssetPath<StyleSheet>("Assets/EditorGUI/Styles/FloatControl.uss", OnStyleLoaded);
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

    // protected override BaseField<float> CreateField() {
    //     return new FloatField();
    // }

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

    void OnStyleLoaded(StyleSheet uss) {
        if (uss != null) {
            styleSheets.Add(uss);
        }
    }
}
}