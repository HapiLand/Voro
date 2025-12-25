#nullable enable
using System;
using System.Reflection;
using UnityEditor;
using UnityEngine;
using VoroSystem.UI.Attributes;
using VoroSystem.UI.Reflection;

namespace VoroSystem.UI {
public class IntField {
    public (bool modified, object? result) ProcessInput(EditorMember member, object? target) {
        var modified = false;
        var number = 0;
        var value = member.GetValue(target);
        if (value != null) {
            number = Convert.ToInt32(value);
        }

        var slider = GetSliderAttribute(member);
        if (slider != null) {
            DrawSlider(ref modified, ref number, slider);
        }
        else {
            var newValue = EditorGUILayout.IntField(member.Name, number);
            if (newValue != number) {
                modified = true;
                number = newValue;
            }
        }

        if (modified && !member.IsReadOnly) {
            member.SetValue(target, number);
        }

        return (modified, number);
    }

    void DrawSlider(ref bool modified, ref int number, SliderAttribute slider) {
        var newValue = EditorGUILayout.IntSlider(
            "",
            number,
            Mathf.RoundToInt(slider.Minimum),
            Mathf.RoundToInt(slider.Maximum));
        if (newValue != number) {
            modified = true;
            number = newValue;
        }
    }

    SliderAttribute? GetSliderAttribute(EditorMember member) {
        // Check if the field or property has a SliderAttribute
        return member.Member.GetCustomAttribute<SliderAttribute>();
    }
}
}