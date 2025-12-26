#nullable enable
using System;
using UnityEditor;
using VoroSystem.UI.Editor.Attributes;
using VoroSystem.UI.Editor.Reflection;

namespace VoroSystem.UI.Editor.Fields {
[Serializable]
[CustomFieldOf(typeof(bool))]
public class ToggleField : CustomField {
  public override (bool modified, object? result) ProcessInput(EditorMember member, object? fieldValue) {
    var modified = false;
    var defaultValue = false;
    if (AttributeHelper.TryGetAttribute(member, out ToggleAttribute? toggleAttr)) {
      defaultValue = toggleAttr.DefaultValue;
    }

    var state = fieldValue is bool b ? b : defaultValue;

    if (member.IsReadOnly) {
      using (new EditorGUI.DisabledScope(true)) {
        EditorGUILayout.Toggle(state);
      }
    }
    else {
      var newValue = EditorGUILayout.Toggle(state);
      modified = newValue != state;
      state = newValue;
    }

    return (modified, state);
  }
}
}