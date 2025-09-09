using System;
using UnityEngine;
using UnityEngine.UIElements;
using VoroEditor.Effects.Internal.enums;
using VoroEditor.Elements;
using Display = VoroEditor.Elements.Display;

namespace VoroEditor.Utility {
public static class UIHelper {
    public static FloatSlider CreateFloatSlider(string name, Func<float> getter, Action<float> setter,
        (float min, float max) range) {
        var slider = new FloatSlider
        {
            DisplayName = name,
            MinValue = range.min,
            MaxValue = range.max
        };
        slider.Bind(getter, setter);
        return slider;
    }

    public static IntSlider CreateIntSlider(string name, Func<int> getter, Action<int> setter,
        (int min, int max) range) {
        var slider = new IntSlider
        {
            DisplayName = name,
            MinValue = range.min,
            MaxValue = range.max
        };
        slider.Bind(getter, setter);
        return slider;
    }

    public static TypeDropdown CreateTypeDropdown(string name, Func<ComputeTypes> getter, Action<ComputeTypes> setter) {
        var dropdown = new TypeDropdown
        {
            DisplayName = name
        };
        dropdown.Bind(getter, setter);
        return dropdown;
    }

    public static Display CreateDisplay(string name) {
        var ve = new Display
        {
            name = name
        };
        return ve;
    }

    public static VisualTreeAsset LoadUxml(string name) {
        var vt = Resources.Load<VisualTreeAsset>(name);

        if (vt != null) {
            return vt;
        }

        Debug.LogError($"{name}.uxml not found");
        return null;
    }

    public static VisualElement Create(string name, string className) {
        var ve = new VisualElement { name = name };
        ve.AddToClassList(className);
        return ve;
    }

    /// <summary>
    ///     a input field for a string
    /// </summary>
    /// <param name="name">label name</param>
    /// <returns>an input where text can be written by the user</returns>
    public static VisualElement CreateEffectStringField(string name) {
        // ToDo implement a UXML for string field
        var ve = new VisualElement { name = name };
        ve.AddToClassList("effect-string-field");
        var label = new Label(name);
        label.AddToClassList("display-label");
        ve.Add(label);
        return ve;
    }

    public static StyleSheet LoadStyleSheet(string path) {
        return ResourceHelper.LoadResource<StyleSheet>(path);
    }
}
}