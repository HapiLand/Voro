using UnityEditor;
using UnityEngine;

namespace VoroSystem.WorldGridSystem.Editor {
public static class WorldGridMenuItem {
    [MenuItem("Voro/Grid/Create World Grid", false, 0)]
    public static void CreateComponent() {
        var existing = Object.FindAnyObjectByType<WorldGridComponent>();
        if (existing != null) {
            Object.DestroyImmediate(existing.gameObject);
        }

        var obj = new GameObject("World Grid");
        obj.AddComponent<WorldGridComponent>();
        Selection.activeObject = obj;
    }
}
}