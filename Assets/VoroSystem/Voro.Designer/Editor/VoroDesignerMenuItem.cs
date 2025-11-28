using UnityEditor;
using UnityEngine;

namespace VoroSystem.Voro.Designer.Editor {
public class VoroDesignerMenuItem : EditorWindow {
  [MenuItem("Voro/Designer/Open Designer")]
  public static void CreateDesigner() {
    Create<VoroDesigner>();
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