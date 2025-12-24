using UnityEditor;
using UnityEngine;

namespace VoroSystem.VoroWorldGeneration.CubicChunks.Cubes.Editor {
/// <summary>
/// displays info about a grid cube
/// </summary>
public class GridCubeWindow : EditorWindow {
  #region Serialized Fields
  [SerializeField] GridCube cube;
  #endregion

  #region Event Functions
  void OnGUI() {
    if (cube == null) {
      EditorGUILayout.LabelField("No GridCube selected");
      return;
    }

    var labelWidth = 150;

    EditorGUILayout.BeginHorizontal();
    EditorGUILayout.LabelField("Position:", GUILayout.Width(labelWidth));
    EditorGUILayout.Vector3Field(GUIContent.none, cube.transform.position);
    EditorGUILayout.EndHorizontal();

    EditorGUILayout.Space();

    EditorGUILayout.BeginHorizontal();
    EditorGUILayout.LabelField("Player Inside:", GUILayout.Width(labelWidth));
    EditorGUILayout.Toggle(cube.CubePlayerDetection?.IsPlayerInside ?? false);
    EditorGUILayout.EndHorizontal();

    EditorGUILayout.Space();

    EditorGUILayout.BeginHorizontal();
    EditorGUILayout.LabelField("Generation:", GUILayout.Width(labelWidth));
    if (GUILayout.Button("Generate Tilemap")) {
      cube.GenerateTilemap();
    }

    EditorGUILayout.EndHorizontal();
  }

  void OnSelectionChange() {
    if (Selection.activeGameObject == null) {
      cube = null;
      Repaint();
      return;
    }

    cube = Selection.activeGameObject.GetComponent<GridCube>();
    Repaint();
  }
  #endregion

  public static void ShowWindow(GridCube cube) {
    var window = GetWindow<GridCubeWindow>("Grid Cube");
    window.cube = cube;
    window.Show();
  }
}
}