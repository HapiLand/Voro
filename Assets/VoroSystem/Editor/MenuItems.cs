using UnityEditor;
using UnityEngine;

namespace VoroSystem.Editor {
public static class MenuItems {
    [MenuItem("Voro/New Mediator")]
    public static void CreateMediator() {
        var existing = Object.FindAnyObjectByType<VoroSystemMediator>();
        if (existing != null) {
            Selection.activeObject = existing.gameObject;
            EditorGUIUtility.PingObject(existing.gameObject);
            return;
        }

        var obj = new GameObject("Voro System Mediator");
        obj.AddComponent<VoroSystemMediator>();
        Selection.activeObject = obj;
    }
}
}