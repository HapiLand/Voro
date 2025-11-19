using UnityEditor;
using UnityEngine;

namespace VoroSystem.Landscape.WorldBoundarySystem.Editor {
[CustomEditor(typeof(WorldBoundaryComponent))]
public class WorldBoundaryDrawer : UnityEditor.Editor {
  // todo https://docs.unity3d.com/6000.2/Documentation/ScriptReference/IMGUI.Controls.BoxBoundsHandle.html
  static WorldBoundaryDrawer() {
    SceneView.duringSceneGui += OnGlobalSceneGUI;
  }

  static void OnGlobalSceneGUI(SceneView sceneView) {
    var existing = FindAnyObjectByType<WorldBoundaryComponent>();
    if (existing != null) {
      Draw(existing);
    }
  }

  static void Draw(WorldBoundaryComponent t) {
    EditorGUI.BeginChangeCheck();
    var cornerA = Handles.PositionHandle(t.Corner.A, Quaternion.identity);
    var cornerB = Handles.PositionHandle(t.Corner.B, Quaternion.identity);

    if (EditorGUI.EndChangeCheck()) {
      Undo.RecordObject(t, "Move Handles");
      t.SetCorners(cornerA, cornerB);
      EditorUtility.SetDirty(t);
      SceneView.RepaintAll();
    }

    Handles.color = Color.white;
    Handles.DrawLine(t.Corner.A, t.Corner.B);
    Handles.DrawWireCube((t.Corner.A + t.Corner.B) * 0.5f,
      new Vector3(Mathf.Abs(t.Corner.B.x - t.Corner.A.x),
        0,
        Mathf.Abs(t.Corner.B.z - t.Corner.A.z)));
  }
}
}