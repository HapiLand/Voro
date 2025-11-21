using UnityEditor;
using UnityEngine;

namespace VoroSystem.Voro.Compute.Editor {
[CustomEditor(typeof(VoroCompute))]
public class ComputeEditor : UnityEditor.Editor {
    #region Serialized Fields
    [SerializeReference] VoroCompute voroCompute;
    #endregion

    public override void OnInspectorGUI() {
        if (!voroCompute) {
            EditorGUILayout.HelpBox("VoroComputeComponent is null or has been destroyed.", MessageType.Warning);
            return;
        }

        serializedObject.Update();

        if (GUILayout.Button("Do Compute")) {
            voroCompute.DoCompute();
        }

        serializedObject.ApplyModifiedProperties();
    }

    #region Event Functions
    void OnEnable() {
        voroCompute = target as VoroCompute;
    }

    void OnDisable() {
        voroCompute = null;
    }
    #endregion
}
}