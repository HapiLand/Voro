using UnityEditor;
using UnityEngine;
using VoroSystem.Compute;
using VoroSystem.Designer;
using VoroSystem.Landscape;
using VoroSystem.Landscape.WorldBoundarySystem;
using VoroSystem.Landscape.WorldGridSystem;
using VoroSystem.Terrain;

namespace VoroSystem.Util.Editor {
public class VoroMenuItems : EditorWindow {
  #region Voro/Debugging

  [MenuItem("Voro/Debugging/Reload", false, 1)]
  static void ReloadDomain() {
    EditorUtility.RequestScriptReload();
  }

  #endregion

  #region Voro/Core

  [MenuItem("Voro/Core/Voro Terrain", false, 0)]
  public static void CreateVoroTerrain() {
    var component = FindAnyObjectByType<VoroTerrainComponent>();
    if (component != null) {
      return;
    }

    Selection.activeObject = new GameObject().AddComponent<VoroTerrainComponent>();
  }

  [MenuItem("Voro/Core/Voro Designer", false, 1)]
  public static void CreateVoroDesigner() {
    var component = FindAnyObjectByType<VoroDesignerComponent>();
    if (component != null) {
      return;
    }

    Selection.activeObject = new GameObject().AddComponent<VoroDesignerComponent>();
  }

  [MenuItem("Voro/Core/Voro Compute", false, 2)]
  public static void CreateVoroCompute() {
    var component = FindAnyObjectByType<VoroComputeComponent>();
    if (component != null) {
      return;
    }

    Selection.activeObject = new GameObject().AddComponent<VoroComputeComponent>();
  }


  [MenuItem("Voro/Core/Internal/Voro Landscape", false, 0)]
  public static void CreateVoroLandscape() {
    var component = FindAnyObjectByType<VoroLandscapeComponent>();
    if (component != null) {
      return;
    }

    Selection.activeObject = new GameObject().AddComponent<VoroLandscapeComponent>();
  }

  #endregion
}
}