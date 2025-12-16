using UnityEditor;
using UnityEngine;

namespace VoroSystem.Voro.Compute.Components.Editor {
public class VoroComputeMenuItem : EditorWindow {
  /*[MenuItem("Voro/Compute/New Compute")]
  public static void CreateCompute() {
    Create<VoroCompute>();
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