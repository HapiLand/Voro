using UnityEditor;
using UnityEngine;
using VoroSystem.VoroWorldGeneration.CubicChunks.Player.Core;

namespace VoroSystem.VoroWorldGeneration.CubicChunks.Cubes.Editor {
public class GridCubeSpawnerWindow : EditorWindow {
  #region Serialized Fields
  [SerializeField] Vector3Int spawnCoordinate = Vector3Int.zero;
  #endregion

  #region Event Functions
  void OnGUI() {
    EditorGUILayout.LabelField("Enter Grid Coordinates:", EditorStyles.boldLabel);

    spawnCoordinate.x = EditorGUILayout.IntField("X", spawnCoordinate.x);
    spawnCoordinate.y = EditorGUILayout.IntField("Y", spawnCoordinate.y);
    spawnCoordinate.z = EditorGUILayout.IntField("Z", spawnCoordinate.z);

    GUILayout.Space(10);

    if (GUILayout.Button("Create")) {
      SpawnCube(spawnCoordinate);
      Close(); // optional: close the window after spawning
    }
  }
  #endregion

  [MenuItem("Voro/Spawn GridCube")]
  public static void ShowWindow() {
    var window = GetWindow<GridCubeSpawnerWindow>("Spawn GridCube");
    window.minSize = new Vector2(250, 100);
  }

  void SpawnCube(Vector3Int coord) {
    var cubeObject = new GameObject($"Cube [{coord.x}, {coord.y}, {coord.z}]");
    cubeObject.transform.position = PlayerLocator.GridToWorld(coord);
    var cube = cubeObject.AddComponent<GridCube>();
    cube.BoundingBox.GridCoord = coord;

    // Select the new object in the hierarchy
    Selection.activeGameObject = cubeObject;
  }
}
}