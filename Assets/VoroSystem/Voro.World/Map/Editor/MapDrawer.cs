using UnityEditor;
using UnityEditor.IMGUI.Controls;
using UnityEngine;
using VoroSystem.Util.Extensions;

namespace VoroSystem.Voro.World.Map.Editor {
[InitializeOnLoad]
[CustomEditor(typeof(VoroMap))]
public class MapDrawer : UnityEditor.Editor {
  static readonly BoxBoundsHandle BoundsHandle = new();

  static MapDrawer() {
    SceneView.duringSceneGui -= OnGlobalSceneGUI;
    SceneView.duringSceneGui += OnGlobalSceneGUI;
  }

  static void OnGlobalSceneGUI(SceneView sceneView) {
    var map = FindAnyObjectByType<VoroMap>();
    if (map != null) {
      DrawHandles(map);
    }
  }

  static void DrawHandles(VoroMap map) {
    var a = map.Corner.A;
    var b = map.Corner.B;

    var min = Vector3.Min(a, b);
    var max = Vector3.Max(a, b);
    var center = (min + max) * 0.5f;
    var size = max - min;

    Handles.color = Color.white;

    EditorGUI.BeginChangeCheck();

    BoundsHandle.center = center;
    BoundsHandle.size = size;
    BoundsHandle.SetColor(Color.white);

    using (new Handles.DrawingScope(Matrix4x4.identity)) {
      BoundsHandle.DrawHandle();
    }

    if (EditorGUI.EndChangeCheck()) {
      Undo.RecordObject(map, "Resize Bounds");

      var newMin = BoundsHandle.center - BoundsHandle.size * 0.5f;
      var newMax = BoundsHandle.center + BoundsHandle.size * 0.5f;

      map.SetCorners(newMin, newMax);
      map.isDirty = true;

      EditorUtility.SetDirty(map);
      SceneView.RepaintAll();
    }

    DrawTiles(map);
  }

  static void DrawTiles(VoroMap map) {
    map.tilemap.ForEach(tile => {
      var center = tile.Position.ToVector3() + new Vector3(tile.Size * 0.5f, 0f, tile.Size * 0.5f);
      const float scale = 0.95f;
      var size = new Vector3(tile.Size * scale, 0f, tile.Size * scale);

      Handles.color = tile.Visible ? Color.forestGreen : Color.crimson;
      Handles.DrawWireCube(center, size);
    });
  }
}
}