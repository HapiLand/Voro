using UnityEditor;
using UnityEngine;

namespace VoroSystem.Voro.Core.Editor {
public class CoreMenu : EditorWindow {
[MenuItem("VoroCore/Core Prefab")]
public static void CreateCorePrefab() {
    var prefab = Resources.Load("CorePrefab") as GameObject;
    var instance = Instantiate(prefab);
    Selection.activeObject = instance;
}

[MenuItem("VoroCore/Landscape Prefab")]
public static void CreateLanscapePrefab() {
    var prefab = Resources.Load("LandscapePrefab") as GameObject;
    var instance = Instantiate(prefab);
    Selection.activeObject = instance;
}
}
}