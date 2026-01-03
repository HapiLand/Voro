using UnityEditor;
using UnityEngine;

namespace VoroSystem.VoroWorldGeneration.CubicChunks.Cubes.Editor {
/// <summary>
/// creates a selectable handle to let the grid cube be selected inside the editors scene
/// </summary>
[CustomEditor(typeof(GridCube))]
public class GridCubeEditor : UnityEditor.Editor {
  #region Event Functions
  void OnSceneGUI() {
    var cube = (GridCube)target;

    cube.GetVisualState(out var baseColor, out var size);
    Handles.color = HandleUtility.nearestControl == GUIUtility.hotControl
      ? Color.white
      : baseColor;

    Handles.color = new Color(Handles.color.r, Handles.color.g, Handles.color.b, 0.05f);

    if (Handles.Button(cube.transform.position, Quaternion.identity, size, size, Handles.CubeHandleCap)) {
      Selection.activeGameObject = cube.gameObject;
      GridCubeWindow.ShowWindow(cube);
    }
  }
  #endregion
}
}