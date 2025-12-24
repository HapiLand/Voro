using System;
using System.Collections.Generic;
using UnityEngine;
using VoroSystem.VoroWorldGeneration.CubicChunks.Cubes;
using VoroSystem.VoroWorldGeneration.CubicChunks.Player;

namespace VoroSystem.VoroWorldGeneration.CubicChunks.World {
public class CubeWorld : GridTracker {
  readonly Dictionary<Vector3Int, GridCube> _cubes = new();
  public static CubeWorld Instance { get; private set; }

  #region Event Functions
  protected override void Awake() {
    base.Awake();
    Instance = this;
  }

  void Start() {
    OnCoordinateChanged(Vector3Int.zero);
  }
  #endregion

  protected override void OnCoordinateChanged(Vector3Int newCoord) {
    base.OnCoordinateChanged(newCoord);
    if (_cubes.TryGetValue(newCoord, out var cube)) {
      CoordinateChanged?.Invoke(cube);
      return;
    }
    var created = CreateCubeAt(newCoord);
    CoordinateChanged?.Invoke(created);
  }
  public event Action<GridCube> CoordinateChanged;
  GridCube CreateCubeAt(Vector3Int coord) {
    var cubeObj = new GameObject($"Cube [{coord.x} {coord.y} {coord.z}]")
    {
      transform =
      {
        position = PlayerLocator.GridToWorld(coord, WorldSettings.GridSize)
      }
    };
    cubeObj.transform.SetParent(transform);
    var cube = cubeObj.AddComponent<GridCube>();
    cube.GridCoord = coord;
    _cubes[coord] = cube;
    return cube;
  }

  public List<GridCube> GetAdjacentCubes(Vector3Int coord) {
    var adjacentCubes = new List<GridCube>();

    // Loop over all offsets in a 3x3x3 cube
    for (var x = -1; x <= 1; x++) {
      for (var y = -1; y <= 1; y++) {
        for (var z = -1; z <= 1; z++) {
          var neighborCoord = coord + new Vector3Int(x, y, z);
          if (_cubes.TryGetValue(neighborCoord, out var cube)) {
            adjacentCubes.Add(cube);
          }
        }
      }
    }

    return adjacentCubes;
  }

  public bool IsValidCoord(Vector3Int neighborCoord) {
    return _cubes.TryGetValue(neighborCoord, out var cube);
  }
}
}