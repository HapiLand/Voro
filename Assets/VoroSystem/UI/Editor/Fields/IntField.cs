#nullable enable
using System;
using UnityEditor;
using VoroSystem.UI.Editor.Attributes;
using VoroSystem.UI.Editor.Reflection;

namespace VoroSystem.UI.Editor.Fields {
[Serializable]
[CustomFieldOf(typeof(int))]
public class IntField : CustomField {
  public override (bool modified, object? result) ProcessInput(EditorMember member, object? fieldValue) {
    var modified = false;
    var number = Convert.ToInt32(fieldValue);

    if (AttributeHelper.TryGetAttribute(member, out SliderAttribute? slider)) {
      DrawSlider(member, ref modified, ref number, slider);
      return (modified, number);
    }

    var newValue = EditorGUILayout.IntField(number);
    modified = newValue != number;
    return (modified, newValue);
  }

  void DrawSlider(EditorMember _, ref bool modified, ref int number, SliderAttribute slider) {
    var newValue = EditorGUILayout.IntSlider(number, slider.Minimum, slider.Maximum);
    modified = newValue != number;
    number = newValue;
  }
}
}