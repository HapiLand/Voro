using System;
using Source;
using UnityEngine;
using VoroSystem.VoroWorldGeneration.CubicChunks.Cubes;
using VoroSystem.VoroWorldGeneration.CubicChunks.Player.Core;

namespace VoroSystem.VoroWorldGeneration.CubicChunks.World {
[Serializable]
public class CubeStorage {
  #region Serialized Fields
  [SerializeField] Transform parent;

  public SerializableDictionary<Vector3Int, GridCube> cubeDictionary = new();
  #endregion

  public CubeStorage(Transform parent) {
    this.parent = parent;
  }

  public bool TryGetCube(Vector3Int coord, out GridCube cube) => cubeDictionary.TryGetValue(coord, out cube);

  public GridCube GetOrCreateCube(Vector3Int coord) =>
    cubeDictionary.TryGetValue(coord, out var cube)
      ? cube
      : CreateCube(coord);

  GridCube CreateCube(Vector3Int coord) {
    var cubeObject = new GameObject($"Cube [{coord.x}, {coord.y}, {coord.z}]");
    cubeObject.transform.SetParent(parent, false);
    cubeObject.transform.position = PlayerLocator.GridToWorld(coord);

    var cube = cubeObject.AddComponent<GridCube>();
    cube.BoundingBox.GridCoord = coord;

    cubeDictionary.Add(coord, cube);
    return cube;
  }
}
}