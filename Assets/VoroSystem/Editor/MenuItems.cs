using UnityEditor;
using UnityEngine;

namespace VoroSystem.Editor {
public static class MenuItems {
    [MenuItem("Voro/Create World Controller")]
    public static void CreateWorldController() {
        var existing = Object.FindAnyObjectByType<WorldController>();
        if (existing != null) {
            Selection.activeObject = existing.gameObject;
            EditorGUIUtility.PingObject(existing.gameObject);
            return;
        }

        var controller = new GameObject("Voro World Controller");
        controller.AddComponent<WorldController>();
        Selection.activeObject = controller;
    }
}
}