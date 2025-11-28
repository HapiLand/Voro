using UnityEditor;
using UnityEngine;

namespace VoroSystem.Voro.Compute.Editor {
[CustomEditor(typeof(VoroCompute))]
public class VoroComputeEditor : UnityEditor.Editor {
  #region Serialized Fields

  [SerializeReference] VoroCompute compute;

  #endregion

  #region Event Functions

  void OnEnable() {
    compute = target as VoroCompute;
  }

  void OnDisable() {
    compute = null;
  }

  #endregion

  public override void OnInspectorGUI() {
    if (!compute) {
      return;
    }

    serializedObject.Update();
    if (GUILayout.Button("Compute")) {
      VoroCompute.OnCompute?.Invoke();
    }

    serializedObject.ApplyModifiedProperties();
  }
}
}