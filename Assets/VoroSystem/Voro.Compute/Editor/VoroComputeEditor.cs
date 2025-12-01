using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using VoroSystem.Voro.Compute.Effects.Core;
using VoroSystem.Voro.Compute.Graphs;

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
    DrawUILine(Color.black);
    if (GUILayout.Button("Compute")) {
      VoroCompute.OnCompute?.Invoke();
    }
    DrawUILine(Color.black);

    serializedObject.ApplyModifiedProperties();
    return;

    void DrawUILine(Color color, int thickness = 1, int padding = 10) {
      var r = EditorGUILayout.GetControlRect(GUILayout.Height(padding + thickness));
      r.height = thickness;
      r.y += padding / 2f;
      r.x -= 2;
      r.width += 6;
      EditorGUI.DrawRect(r, color);
    }
  }
}
}