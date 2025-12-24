using UnityEditor;
using UnityEngine;
using VoroSystem.VoroWorldGeneration.CubicChunks.Player.Core;
using VoroSystem.VoroWorldGeneration.CubicChunks.World.Core;

namespace VoroSystem.VoroWorldGeneration.CubicChunks.Cubes.Editor {
/// <summary>
/// displays info about a grid cube 
/// </summary>
public class GridCubeWindow : EditorWindow {
  GridCube _cube;

  #region Event Functions
  void OnGUI() {
    if (_cube == null) {
      EditorGUILayout.LabelField("No GridCube selected");
      return;
    }

    var labelWidth = 150;
    
    EditorGUILayout.BeginHorizontal();
    EditorGUILayout.LabelField("Position:", GUILayout.Width(labelWidth));
    EditorGUILayout.Vector3Field(GUIContent.none, _cube.transform.position);
    EditorGUILayout.EndHorizontal();

    EditorGUILayout.Space();

    EditorGUILayout.BeginHorizontal();
    EditorGUILayout.LabelField("Player Inside:", GUILayout.Width(labelWidth));
    EditorGUILayout.Toggle(_cube.CubePlayerDetection?.IsPlayerInside ?? false);
    EditorGUILayout.EndHorizontal();
    
    EditorGUILayout.Space();
    
    EditorGUILayout.BeginHorizontal();
    EditorGUILayout.LabelField("Generation:", GUILayout.Width(labelWidth));
    if (GUILayout.Button("Generate Tilemap")) {
      _cube.GenerateTilemap();
    }
    EditorGUILayout.EndHorizontal();
  }

  void OnSelectionChange() {
    if (Selection.activeGameObject == null) {
      _cube = null;
      Repaint();
      return;
    }
    
    _cube = Selection.activeGameObject.GetComponent<GridCube>();
    Repaint();
  }
  #endregion

  public static void ShowWindow(GridCube cube) {
    var window = GetWindow<GridCubeWindow>("Grid Cube");
    window._cube = cube;
    window.Show();
  }
  

}
}