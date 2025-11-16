using UnityEditor;
using UnityEngine;
using VoroSystem.Extensions;

namespace VoroSystem.Landscape.WorldMapSystem.Editor {
[CustomEditor(typeof(WorldTilemapComponent))]
public class WorldTilemapDrawer : UnityEditor.Editor {
    static WorldTilemapDrawer() {
        SceneView.duringSceneGui += OnGlobalSceneGUI;
    }

    static void OnGlobalSceneGUI(SceneView sceneView) {
        var existing = FindAnyObjectByType<WorldTilemapComponent>();
        if (existing != null) {
            Draw(existing);
        }
    }

    static void Draw(WorldTilemapComponent t) {
        t.ForEach(tile => {
            var center = tile.position.ToVector3() + new Vector3(tile.size / 2f, 0f, tile.size / 2f);
            var size = new Vector3(tile.size * 0.25f, 0f, tile.size * 0.25f);
            Handles.color = tile.Visible ? Color.seaGreen : Color.coral;
            Handles.color *= new Color(1, 1, 1, 0.2f);
            Handles.DrawWireCube(center, size);
        });
    }
}
}