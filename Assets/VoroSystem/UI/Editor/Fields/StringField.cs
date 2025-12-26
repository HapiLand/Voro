#nullable enable
using System;
using UnityEditor;
using UnityEngine;
using VoroSystem.UI.Editor.Attributes;
using VoroSystem.UI.Editor.Reflection;

namespace VoroSystem.UI.Editor.Fields {
[Serializable]
[CustomFieldOf(typeof(string))]
public class StringField : CustomField {
  public override (bool modified, object? result) ProcessInput(EditorMember member, object? fieldValue) {
    var modified = false;
    var defaultValue = "";
    if (AttributeHelper.TryGetAttribute(member, out TextAttribute? textAttr)) {
      defaultValue = textAttr.DefaultValue;
    }

    var text = fieldValue as string ?? defaultValue;

    if (member.IsReadOnly) {
      GUILayout.Label(text);
    }
    else {
      var newValue = EditorGUILayout.TextField(text);
      modified = newValue != text;
      text = newValue;
    }

    return (modified, text);
  }
}
}