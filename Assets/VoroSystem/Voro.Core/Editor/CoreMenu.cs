using UnityEditor;
using UnityEngine;
using VoroSystem.Voro.Compute;
using VoroSystem.Voro.Designer;
using VoroSystem.Voro.World;

namespace VoroSystem.Voro.Core.Editor {
public class CoreMenu : EditorWindow {
  [MenuItem("VoroCore/New Compute")]
  public static void CreateCompute() {
    var designer = Create<VoroDesigner>();
    var compute = Create<VoroCompute>();
    compute.Init(designer);
  }

  [MenuItem("VoroCore/Open Designer")]
  public static void CreateDesigner() {
    Create<VoroWorld>();
    Create<VoroDesigner>();
  }

  [MenuItem("VoroCore/New World")]
  public static void CreateWorld() {
    Create<VoroWorld>();
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