using UnityEditor;
using UnityEngine;
using VoroSystem.VoroDataStructures.EffectDefinition.ParameterDefinition.Core;
using VoroSystem.VoroDataStructures.EffectDefinition.ParameterDefinition.Variants;
using VoroSystem.VoroWorldGeneration;

namespace VoroSystem.VoroGraphEditor.Editor.Drawers {
[CustomPropertyDrawer(typeof(ParameterData), true)]
public class ParameterDataDrawer : PropertyDrawer {
  const float Spacing = 5f;
  const float NameWidth = 80f;
  const float VariantWidth = 80f;

  public override void OnGUI(Rect position, SerializedProperty property, GUIContent label) {
    EditorGUI.BeginProperty(position, label, property);
    position = EditorGUI.PrefixLabel(position, GUIUtility.GetControlID(FocusType.Passive), label);
    var indent = EditorGUI.indentLevel;
    EditorGUI.indentLevel = 0;

    var didChange = DrawFields(position, property);
    if (didChange) {
      WorldGenEditorEvents.RaiseParametersChanged();
    }

    EditorGUI.indentLevel = indent;
    EditorGUI.EndProperty();
  }

  bool DrawFields(Rect position, SerializedProperty property) {
    EditorGUI.BeginChangeCheck();

    var nameRect = GetNameRect(position);
    var variantRect = GetVariantRect(position);
    var usedWidth = nameRect.width + Spacing + variantRect.width + Spacing;
    var valueRect = GetValueRect(position, usedWidth);

    EditorGUI.PropertyField(nameRect, property.FindPropertyRelative("parameterName"), GUIContent.none);
    EditorGUI.PropertyField(variantRect, property.FindPropertyRelative("parameterType"), GUIContent.none);

    var defaultValueProp = property.FindPropertyRelative("defaultValue");
    if (defaultValueProp.managedReferenceValue == null) {
      EditorGUI.HelpBox(valueRect, "Default value is NULL", MessageType.Warning);
      return EditorGUI.EndChangeCheck();
    }

    var valueProp = defaultValueProp.FindPropertyRelative("value");
    if (valueProp == null) {
      Debug.LogError(
        $"ParameterDrawer: 'value' field not found in {defaultValueProp.managedReferenceValue.GetType().Name}");
      EditorGUI.HelpBox(valueRect, "Missing 'value' field", MessageType.Error);
      return EditorGUI.EndChangeCheck();
    }

    switch (defaultValueProp.managedReferenceValue) {
    case FloatValue:
      EditorGUI.PropertyField(valueRect, valueProp, GUIContent.none);
      break;

    case BoolValue:
      valueProp.boolValue = EditorGUI.Toggle(valueRect, valueProp.boolValue);
      break;

    default:
      EditorGUI.HelpBox(
        valueRect,
        $"Unsupported type: {defaultValueProp.managedReferenceValue.GetType().Name}",
        MessageType.Warning
      );
      break;
    }

    return EditorGUI.EndChangeCheck();
  }

  static Rect GetNameRect(Rect position) => new(position.x, position.y, NameWidth, position.height);

  static Rect GetVariantRect(Rect position) =>
    new(position.x + NameWidth + Spacing, position.y, VariantWidth, position.height);

  static Rect GetValueRect(Rect position, float usedWidth) {
    var valueWidth = position.width - usedWidth - Spacing * 2;
    return new Rect(position.x + usedWidth + Spacing, position.y, valueWidth, position.height);
  }
}
}