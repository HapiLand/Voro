using System;
using System.Collections.Generic;
using UnityEngine;
using VoroSystem.VoroWorldGeneration.CubicChunks.Cubes;
using VoroSystem.VoroWorldGeneration.CubicChunks.Player;
using VoroSystem.VoroWorldGeneration.CubicChunks.Player.Core;
using VoroSystem.VoroWorldGeneration.CubicChunks.World.Core;

namespace VoroSystem.VoroWorldGeneration.CubicChunks.World {
public class CubeWorld : GridTracker {
  readonly Dictionary<Vector3Int, GridCube> _cubes = new();
  public static CubeWorld Instance { get; private set; }

  #region Event Functions
  protected override void Awake() {
    base.Awake();

    if (Instance != null && Instance != this) {
      Destroy(gameObject);
      return;
    }

    Instance = this;
  }
  #endregion

  public event Action<GridCube> CoordinateChanged;

  protected override void OnCoordinateChanged(Vector3Int newCoord) {
    base.OnCoordinateChanged(newCoord);
    var cube = GetOrCreateCube(newCoord);
    CoordinateChanged?.Invoke(cube);
  }

  public bool TryGetCube(Vector3Int coord, out GridCube cube) {
    return _cubes.TryGetValue(coord, out cube);
  }

  public IEnumerable<GridCube> GetAdjacentCubes(Vector3Int coord) {
    for (var x = -1; x <= 1; x++)
    for (var y = -1; y <= 1; y++)
    for (var z = -1; z <= 1; z++) {
      if (x == 0 && y == 0 && z == 0) {
        continue;
      }

      var neighborCoord = coord + new Vector3Int(x, y, z);

      if (_cubes.TryGetValue(neighborCoord, out var cube)) {
        yield return cube;
      }
    }
  }

  GridCube GetOrCreateCube(Vector3Int coord) {
    return _cubes.TryGetValue(coord, out var cube) ? cube : CreateCube(coord);
  }

  GridCube CreateCube(Vector3Int coord) {
    var cubeObject = new GameObject($"Cube [{coord.x}, {coord.y}, {coord.z}]");
    cubeObject.transform.SetParent(transform, false);
    cubeObject.transform.position = PlayerLocator.GridToWorld(coord, WorldSettings.GridSize);

    var cube = cubeObject.AddComponent<GridCube>();
    cube.BoundingBox.GridCoord = coord;

    _cubes.Add(coord, cube);
    return cube;
  }
}
}