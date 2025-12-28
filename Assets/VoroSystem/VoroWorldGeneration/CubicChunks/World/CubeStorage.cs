using System;
using Source;
using UnityEngine;
using Voro.Internal.World;
using VoroSystem.VoroWorldGeneration.CubicChunks.Player.Core;

namespace VoroSystem.VoroWorldGeneration.CubicChunks.World {
[Serializable]
public class CubeStorage {
  #region Serialized Fields
  [SerializeField] Transform parent;

  public SerializableDictionary<Vector3Int, Chunk> cubeDictionary = new();
  #endregion

  public CubeStorage(Transform parent) {
    this.parent = parent;
  }

  public bool TryGetCube(Vector3Int coord, out Chunk cube) {
    return cubeDictionary.TryGetValue(coord, out cube);
  }

  public Chunk GetOrCreateCube(Vector3Int coord) {
    return cubeDictionary.TryGetValue(coord, out var cube)
      ? cube
      : CreateCube(coord);
  }

  Chunk CreateCube(Vector3Int coord) {
    var cubeObject = new GameObject($"Cube [{coord.x}, {coord.y}, {coord.z}]");
    cubeObject.transform.SetParent(parent, false);
    cubeObject.transform.position = PlayerLocator.GridToWorld(coord);

    var cube = cubeObject.AddComponent<Chunk>();
    cube.GridCoord = coord;

    cubeDictionary.Add(coord, cube);
    return cube;
  }
}
}