using UnityEditor;
using UnityEngine;

namespace VoroSystem.Voro.Core.Editor {
public class CoreMenu : EditorWindow {
  [MenuItem("Voro/Debug/Reload Domain")]
  static void ReloadDomain() {
    EditorUtility.RequestScriptReload();
  }

  [MenuItem("Voro/New Core", false, 0)]
  static void CreateCore() {
    Create<VoroCore>();
  }

  static T Create<T>() where T : Component {
    var component = FindAnyObjectByType<T>();
    if (component != null) {
      return component;
    }

    component = new GameObject().AddComponent<T>();
    Selection.activeObject = component;
    return component;
  }
}
}