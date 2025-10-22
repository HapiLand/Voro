using UnityEditor;
using UnityEngine;
using Voro.Core.VoroSystem;

namespace Voro.Editor {
public static class MenuItems {
    [MenuItem("Voro/Create Overseer")]
    public static void CreateOverseer() {
        var existing = Object.FindAnyObjectByType<VoroUnityComponent>();
        if (existing != null) {
            Object.DestroyImmediate(existing.gameObject);
            // Selection.activeObject = existing.gameObject;
            // EditorGUIUtility.PingObject(existing.gameObject);
            // return;
        }

        var obj = new GameObject("Overseer");
        obj.AddComponent<VoroUnityComponent>();
        Selection.activeObject = obj;
    }
}
}