using UnityEditor;
using VoroSystem.VoroGraphEditor.Data;

// ReSharper disable RequiredBaseTypesIsNotInherited

namespace VoroSystem.VoroGraphEditor.Editor.Drawers {
[CustomPropertyDrawer(typeof(GraphScriptableObject))]
public class GraphDrawer : UnityEditor.Editor {
  SerializedProperty _graphNameProp;
  SerializedProperty _layersProp;

  #region Event Functions
  void OnEnable() {
    _graphNameProp = serializedObject.FindProperty("graphName");
    _layersProp = serializedObject.FindProperty("layers");
  }
  #endregion

  public override void OnInspectorGUI() {
    serializedObject.Update();

    EditorGUILayout.PropertyField(_graphNameProp);

    EditorGUILayout.Space();
    EditorGUILayout.LabelField("Layers", EditorStyles.boldLabel);

    EditorGUILayout.PropertyField(_layersProp, true);

    serializedObject.ApplyModifiedProperties();
  }
}
}