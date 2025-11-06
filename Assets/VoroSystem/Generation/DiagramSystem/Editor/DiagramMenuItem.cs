using UnityEditor;
using UnityEngine;

namespace VoroSystem.Generation.DiagramSystem.Editor {
public static class DiagramMenuItem {
    [MenuItem("Voro/Diagram/Create Diagram", false, 0)]
    public static void CreateComponent() {
        var existing = Object.FindAnyObjectByType<DiagramComponent>();
        if (existing != null) {
            Object.DestroyImmediate(existing.gameObject);
        }

        var obj = new GameObject("Diagram");
        obj.AddComponent<DiagramComponent>();
        Selection.activeObject = obj;
    }
}
}