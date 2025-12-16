using UnityEditor;
using UnityEngine;

namespace VoroSystem.Voro.Core.Components.Editor {
public class CoreMenu : EditorWindow {
  [MenuItem("VoroNew/Reload Domain")]
  static void ReloadDomain() {
    EditorUtility.RequestScriptReload();
  }

  [MenuItem("VoroNew/Core")]
  static void CreateCore() {
    CoreEditorWindow.ShowWindow();
    Create<VoroCore>();
  }

  static T Create<T>() where T : Component {
    var component = FindAnyObjectByType<T>();
    if (component != null) {
      DestroyImmediate(component.gameObject);
    }

    component = new GameObject().AddComponent<T>();
    Selection.activeObject = component;
    return component;
  }
}
}