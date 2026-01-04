using UnityEditor;
using UnityEditorInternal;
using UnityEngine;
using VoroSystem.VoroDataStructures.EffectDefinition.Core;

namespace VoroSystem.VoroGraphEditor.Editor.Drawers {
[CustomPropertyDrawer(typeof(EffectData))]
public class EffectDataDrawer : PropertyDrawer {
  const float Spacing = 5f;
  const float HalfWidthFactor = 0.5f;
  EffectVariants _lastEffectVariant;
  ReorderableList _list;

  public override void OnGUI(Rect position, SerializedProperty property, GUIContent label) {
    EditorGUI.BeginProperty(position, label, property);
    position = EditorGUI.PrefixLabel(position, GUIUtility.GetControlID(FocusType.Passive), label);
    var indent = EditorGUI.indentLevel;
    EditorGUI.indentLevel = 0;

    DrawFields(position, property);

    EditorGUI.indentLevel = indent;
    EditorGUI.EndProperty();
  }

  void DrawFields(Rect position, SerializedProperty property) {
    var lineHeight = EditorGUIUtility.singleLineHeight;

    var variantRect = GetVariantRect(position, lineHeight);
    var operationRect = GetOperationRect(position, lineHeight);
    var parametersRect = GetParametersRect(position, lineHeight);

    var effectTypeProp = property.FindPropertyRelative("effectType");
    EditorGUI.BeginChangeCheck();
    EditorGUI.PropertyField(variantRect, effectTypeProp, GUIContent.none);
    if (EditorGUI.EndChangeCheck()) {
      var newValue = (EffectVariants)effectTypeProp.enumValueIndex;
      if (!newValue.Equals(_lastEffectVariant)) {
        _lastEffectVariant = newValue;
        Debug.Log($"New effectType value: {newValue}");
      }
    }

    EditorGUI.PropertyField(variantRect, property.FindPropertyRelative("effectType"), GUIContent.none);

    EditorGUI.PropertyField(operationRect, property.FindPropertyRelative("operationType"), GUIContent.none);

    if (_list == null) {
      _list = new ReorderableList(property.serializedObject, property.FindPropertyRelative("parameters"),
        false, false, false, false);
      _list.drawElementCallback = (rect, index, isActive, isFocused) => {
        var element = _list.serializedProperty.GetArrayElementAtIndex(index);
        rect.height = EditorGUI.GetPropertyHeight(element);
        EditorGUI.PropertyField(rect, element, GUIContent.none);
      };
    }

    _list.DoList(parametersRect);
  }

  static Rect GetVariantRect(Rect position, float lineHeight) {
    var halfWidth = (position.width - Spacing) * HalfWidthFactor;
    return new Rect(position.x, position.y, halfWidth, lineHeight);
  }

  static Rect GetOperationRect(Rect position, float lineHeight) {
    var halfWidth = (position.width - Spacing) * HalfWidthFactor;
    return new Rect(position.x + halfWidth + Spacing, position.y, halfWidth, lineHeight);
  }

  static Rect GetParametersRect(Rect position, float lineHeight) =>
    new(position.x, position.y + lineHeight + Spacing, position.width, lineHeight);

  public override float GetPropertyHeight(SerializedProperty property, GUIContent label) {
    var lineHeight = EditorGUIUtility.singleLineHeight;

    var parametersProp = property.FindPropertyRelative("parameters");
    var parametersHeight = EditorGUI.GetPropertyHeight(parametersProp, true);

    return lineHeight + Spacing + parametersHeight;
  }
}
}