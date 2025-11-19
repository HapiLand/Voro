using OldVoroSystem.Generation;
using OldVoroSystem.Landscape;
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

  #region Voro/Old

  [MenuItem("Voro/Old/Create Diagram", false, 2)]
  public static void CreateDiagram() {
    var existing = FindAnyObjectByType<DiagramComponent>();
    if (existing != null) {
      DestroyImmediate(existing.gameObject);
    }

    var obj = new GameObject("Diagram");
    obj.AddComponent<DiagramComponent>();
    Selection.activeObject = obj;
  }

  [MenuItem("Voro/Old/Create Tilemap", false, 2)]
  public static void CreateTilemap() {
    var existing = FindAnyObjectByType<TilemapComponent>();
    if (existing != null) {
      DestroyImmediate(existing.gameObject);
    }

    var obj = new GameObject("Tilemap");
    obj.AddComponent<TilemapComponent>();
    Selection.activeObject = obj;
  }

  [MenuItem("Voro/Old/Create World Boundary", false, 2)]
  public static void CreateWorldBoundary() {
    var obj = new GameObject("World Boundary");
    obj.AddComponent<WorldBoundaryComponent>();
    Selection.activeObject = obj;
  }

  [MenuItem("Voro/Old/Create World Grid", false, 2)]
  public static void CreateWorldGrid() {
    var existing = FindAnyObjectByType<WorldGridComponent>();
    if (existing != null) {
      DestroyImmediate(existing.gameObject);
    }

    var obj = new GameObject("World Grid");
    obj.AddComponent<WorldGridComponent>();
    Selection.activeObject = obj;
  }

  #endregion
}
}