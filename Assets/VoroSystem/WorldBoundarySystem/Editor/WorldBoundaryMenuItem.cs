using UnityEditor;
using UnityEngine;

namespace VoroSystem.WorldBoundarySystem.Editor {
public static class WorldBoundaryMenuItem {
    [MenuItem("Voro/Bounds/Create World Boundary")]
    public static void CreateComponent() {
        var existing = Object.FindAnyObjectByType<WorldBoundaryComponent>();
        if (existing != null) {
            Object.DestroyImmediate(existing.gameObject);
        }

        var obj = new GameObject("World Boundary");
        obj.AddComponent<WorldBoundaryComponent>();
        Selection.activeObject = obj;
    }
}
}