#nullable enable
using System;
using UnityEngine;
using VoroSystem.UI.Editor.Attributes;
using VoroSystem.UI.Editor.Reflection;

namespace VoroSystem.UI.Editor.Fields {
[Serializable]
[CustomFieldOf(typeof(Enum))]
public class EnumField : CustomField {
  public override (bool modified, object? result) ProcessInput(EditorMember member, object? fieldValue) {
    var modified = false;
    GUILayout.Label("Dsajkdfsaksd");
    if (!AttributeHelper.TryGetAttribute(member, out VariantAttribute? enumAttr)) {
      return (false, fieldValue);
    }

    var enumType = enumAttr.EnumType;
    var defaultValue = enumAttr.DefaultValue;

    var state = fieldValue != null && fieldValue.GetType() == enumType ? (Enum)fieldValue : (Enum)defaultValue;
    if (member.IsReadOnly) {
      // GUILayout.Label(member.Name, state.ToString());
    }

    // var newValue = EditorGUILayout.EnumPopup(state);
    // modified = !Equals(newValue, state);
    // state = newValue;
    return (modified, state);
  }
}
}