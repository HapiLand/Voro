using System;
using EditorGUI.Source.Utility;
using UnityEngine.UIElements;

namespace OLD.VoroEditor.Elements {
[UxmlElement]
public partial class IntSlider : VisualElement {
    readonly IntegerField _field;
    readonly Label _label;
    readonly SliderInt _slider;
    Func<int> _getter;
    Action<int> _setter;
    int _value; // current value of the slider

    public IntSlider() {
        // instantiate the uxml
        var vt = UIHelper.LoadUxml("IntSlider");
        vt.CloneTree(this);

        // query each element
        _label = this.Q<Label>("Label");
        _field = this.Q<IntegerField>("Field");
        _slider = this.Q<SliderInt>("Slider");

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
    public void Bind(Func<int> getter, Action<int> setter) {
        _getter = getter;
        _setter = setter;
        Refresh();
    }

    /// <summary>
    ///     refresh the slider whenever the data changes externally
    /// </summary>
    public void Refresh() {
        if (_getter != null) {
            // clamp in range
            var value = Math.Max(_slider.lowValue, Math.Min(_slider.highValue, _getter()));
            _slider?.SetValueWithoutNotify(value);
            _field?.SetValueWithoutNotify(value);
        }
    }

    void SetValueWithoutNotify(int value) {
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
    public int Value {
        get => _getter?.Invoke() ?? 0;
        set => SetValueWithoutNotify(value);
    }

    [UxmlAttribute]
    public int MinValue {
        get => _slider?.lowValue ?? 0;
        set => _slider.lowValue = value;
    }

    [UxmlAttribute]
    public int MaxValue {
        get => _slider?.highValue ?? 1;
        set => _slider.highValue = value;
    }

    [UxmlAttribute] public bool IsLogarithmic { get; set; }
    // ToDo implement logarithmic scale

    #endregion
}
}