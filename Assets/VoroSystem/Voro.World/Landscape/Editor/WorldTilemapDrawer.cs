using UnityEditor;
using UnityEngine;
using VoroSystem.Util.Extensions;
using VoroSystem.Voro.World.Landscape.Map;

namespace VoroSystem.Voro.World.Landscape.Editor {
[CustomEditor(typeof(WorldTilemap))]
public class WorldTilemapDrawer : UnityEditor.Editor {
  static WorldTilemapDrawer() {
    SceneView.duringSceneGui += OnGlobalSceneGUI;
  }

  static void OnGlobalSceneGUI(SceneView sceneView) {
    var existing = FindAnyObjectByType<WorldTilemap>();
    if (existing != null) {
      Draw(existing);
    }
  }

  static void Draw(WorldTilemap t) {
    t.ForEach(tile => {
      var center = tile.position.ToVector3() + new Vector3(tile.size / 2f, 0f, tile.size / 2f);
      var scale = 0.95f;
      var size = new Vector3(tile.size * scale, 0f, tile.size * scale);
      Handles.color = tile.Visible ? Color.seaGreen : Color.coral;
      Handles.color *= new Color(1, 1, 1, 0.2f);
      Handles.DrawWireCube(center, size);
    });
  }
}
}