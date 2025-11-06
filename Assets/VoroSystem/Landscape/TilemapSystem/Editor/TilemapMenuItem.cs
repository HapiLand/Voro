using UnityEditor;
using UnityEngine;

namespace VoroSystem.Landscape.TilemapSystem.Editor {
public static class TilemapMenuItem {
    [MenuItem("Voro/Tilemap/Create Tilemap")]
    public static void CreateComponent() {
        var existing = Object.FindAnyObjectByType<TilemapComponent>();
        if (existing != null) {
            Object.DestroyImmediate(existing.gameObject);
        }

        var obj = new GameObject("Tilemap");
        obj.AddComponent<TilemapComponent>();
        Selection.activeObject = obj;
    }
}
}