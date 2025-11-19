using UnityEditor;
using UnityEngine;

namespace VoroSystem.Compute.Editor {
[CustomEditor(typeof(VoroComputeComponent))]
public class VoroComputeComponentEditor : UnityEditor.Editor {
  #region Serialized Fields

  [SerializeReference] VoroComputeComponent voroComputeComponent;

  #endregion

  #region Event Functions

  void OnEnable() {
    voroComputeComponent = target as VoroComputeComponent;
  }

  void OnDisable() {
    voroComputeComponent = null;
  }

  #endregion

  public override void OnInspectorGUI() {
    if (!voroComputeComponent) {
      EditorGUILayout.HelpBox("VoroComputeComponent is null or has been destroyed.", MessageType.Warning);
      return;
    }

    serializedObject.Update();

    if (GUILayout.Button("Do Compute")) {
      voroComputeComponent.DoCompute();
    }

    serializedObject.ApplyModifiedProperties();
  }
}
}