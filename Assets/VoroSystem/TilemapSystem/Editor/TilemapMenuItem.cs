using UnityEditor;
using UnityEngine;

namespace VoroSystem.TilemapSystem.Editor {
public static class TilemapMenuItem {
    [MenuItem("Voro/Tilemap/Create Tilemap")]
    public static void CreateComponent() {
        var existing = Object.FindAnyObjectByType<BasicTilemapComponent>();
        if (existing != null) {
            Object.DestroyImmediate(existing.gameObject);
        }

        var obj = new GameObject("Tilemap");
        obj.AddComponent<BasicTilemapComponent>();
        Selection.activeObject = obj;
    }
}
}