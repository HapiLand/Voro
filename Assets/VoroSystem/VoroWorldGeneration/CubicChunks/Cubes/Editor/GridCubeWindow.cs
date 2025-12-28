using UnityEditor;
using UnityEngine;
using Voro.Internal.World;

namespace VoroSystem.VoroWorldGeneration.CubicChunks.Cubes.Editor {
/// <summary>
/// displays info about a grid cube
/// </summary>
public class GridCubeWindow : EditorWindow {
  #region Serialized Fields
  [SerializeField] Chunk cube;
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
    EditorGUILayout.Toggle(cube.IsPlayerInside);
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

    cube = Selection.activeGameObject.GetComponent<Chunk>();
    Repaint();
  }
  #endregion

  public static void ShowWindow(Chunk cube) {
    var window = GetWindow<GridCubeWindow>("Grid Cube");
    window.cube = cube;
    window.Show();
  }
}
}