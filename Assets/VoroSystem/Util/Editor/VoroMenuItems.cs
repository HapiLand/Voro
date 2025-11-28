using UnityEditor;
using UnityEngine;
using VoroSystem.Voro.Compute;
using VoroSystem.Voro.Designer;
using VoroSystem.Voro.World.Map;

namespace VoroSystem.Util.Editor {
public class VoroMenuItems : EditorWindow {
  #region Voro/Debugging

  [MenuItem("Voro/Debugging/Reload", false, 1)]
  static void ReloadDomain() {
    EditorUtility.RequestScriptReload();
  }

  #endregion

  #region Voro/Core

  [MenuItem("Voro/Core/Voro Designer", false, 1)]
  public static void CreateVoroDesigner() {
    var component = FindAnyObjectByType<VoroDesigner>();
    if (component != null) {
      return;
    }

    Selection.activeObject = new GameObject().AddComponent<VoroDesigner>();
  }

  [MenuItem("Voro/Core/Voro Compute", false, 2)]
  public static void CreateVoroCompute() {
    var component = FindAnyObjectByType<VoroCompute>();
    if (component != null) {
      return;
    }

    Selection.activeObject = new GameObject().AddComponent<VoroCompute>();
  }


  [MenuItem("Voro/Core/Internal/Voro Landscape", false, 0)]
  public static void CreateVoroLandscape() {
    var component = FindAnyObjectByType<VoroMap>();
    if (component != null) {
      return;
    }

    Selection.activeObject = new GameObject().AddComponent<VoroMap>();
  }

  #endregion
}
}