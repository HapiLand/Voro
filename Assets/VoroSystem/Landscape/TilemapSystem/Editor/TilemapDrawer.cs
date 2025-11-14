using UnityEditor;
using UnityEngine;
using VoroSystem.Extensions;

namespace VoroSystem.Landscape.TilemapSystem.Editor {
[CustomEditor(typeof(TilemapComponent))]
public class TilemapDrawer : UnityEditor.Editor {
    static TilemapDrawer() {
        SceneView.duringSceneGui += OnGlobalSceneGUI;
    }

    static void OnGlobalSceneGUI(SceneView sceneView) {
        var existing = FindAnyObjectByType<TilemapComponent>();
        if (existing != null) {
            Draw(existing);
        }
    }

    static void Draw(TilemapComponent t) {
        t.ForEach(tile => {
            var center = tile.Position.ToVector3() + new Vector3(tile.Size / 2f, 0f, tile.Size / 2f);
            var size = new Vector3(tile.Size * 0.25f, 0f, tile.Size * 0.25f);
            Handles.color = tile.Visible ? Color.seaGreen : Color.coral;
            Handles.color *= new Color(1, 1, 1, 0.2f);
            Handles.DrawWireCube(center, size);
        });
    }
}
}