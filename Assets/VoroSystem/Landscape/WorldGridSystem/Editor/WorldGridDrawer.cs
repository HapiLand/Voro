using UnityEditor;
using UnityEngine;

namespace VoroSystem.Landscape.WorldGridSystem.Editor {
[CustomEditor(typeof(WorldGridComponent))]
public class WorldGridDrawer : UnityEditor.Editor {
    static WorldGridDrawer() {
        SceneView.duringSceneGui += OnGlobalSceneGUI;
    }

    static void OnGlobalSceneGUI(SceneView sceneView) {
        var existing = FindAnyObjectByType<WorldGridComponent>();
        if (existing != null) {
            Draw(existing);
        }
    }

    static void Draw(WorldGridComponent t) {
        return;
        var (sizeX, sizeZ, gridSize) = t.Dimensions;
        var origin = t.Origin;

        for (var z = 0; z < sizeZ; z++) {
            for (var x = 0; x < sizeX; x++) {
                var center =
                    origin
                    + new Vector3(x * gridSize + gridSize / 2f, 0f, z * gridSize + gridSize / 2f);
                var size = new Vector3(gridSize * 0.95f, 0f, gridSize * 0.95f);
                Handles.color = Color.lawnGreen;
                Handles.DrawWireCube(center, size);
            }
        }
    }
}
}