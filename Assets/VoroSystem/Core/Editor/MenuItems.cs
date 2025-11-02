using UnityEditor;
using UnityEngine;

namespace VoroSystem.Core.Editor {
public static class MenuItems {
    [MenuItem("Voro/Create Voro")]
    public static void CreateVoroComponent() {
        var existing = Object.FindAnyObjectByType<VoroComponent>();
        if (existing != null) {
            Object.DestroyImmediate(existing.gameObject);
        }

        var obj = new GameObject("Voro");
        obj.AddComponent<VoroComponent>();
        Selection.activeObject = obj;
    }
}
}