using UnityEditor;
using UnityEngine;

namespace VoroSystem.Voro.World.Components.Editor {
public class VoroWorldMenu : EditorWindow {
  /*[MenuItem("Voro/World/New World")]
  public static void CreateWorld() {
    Create<VoroWorld>();
  }*/

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