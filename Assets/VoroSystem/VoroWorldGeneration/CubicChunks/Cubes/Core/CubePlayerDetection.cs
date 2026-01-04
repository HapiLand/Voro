using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using VoroSystem.VoroWorldGeneration.CubicChunks.Player;
using VoroSystem.VoroWorldGeneration.CubicChunks.World;

namespace VoroSystem.VoroWorldGeneration.CubicChunks.Cubes.Core {
[Serializable]
public class CubePlayerDetection {
  static readonly Vector3Int[] NeighborOffsets = GenerateNeighborOffsets();

  #region Serialized Fields
  [SerializeField] bool lastPlayerInside;

  [field: SerializeField] public bool IsPlayerInside { get; private set; }
  [field: SerializeField] public bool NeighborHasPlayer { get; private set; }
  [field: SerializeField] public PlayerPoint Player { get; set; }

  [SerializeField] GridCube cube;
  #endregion


  public CubePlayerDetection(GridCube cube) {
    this.cube = cube;
  }

  public void Update() {
    if (!Player) {
      SetPlayerInside(false);
      return;
    }

    var inside = cube.BoundingBox.Bounds.Contains(Player.transform.position);
    SetPlayerInside(inside);
  }

  void SetPlayerInside(bool inside) {
    if (inside == lastPlayerInside) {
      return;
    }

    IsPlayerInside = inside;
    lastPlayerInside = inside;
    NotifyNeighbors(inside);
  }

  void NotifyNeighbors(bool playerInside) {
    foreach (var neighbor in GetNeighbors()) {
      neighbor.CubePlayerDetection.SetNeighborHasPlayer(playerInside);
    }
  }

  public void SetNeighborHasPlayer(bool value) {
    NeighborHasPlayer = value;
  }


  static Vector3Int[] GenerateNeighborOffsets() =>
    (from x in Enumerable.Range(-1, 3)
      from y in Enumerable.Range(-1, 3)
      from z in Enumerable.Range(-1, 3)
      where x != 0 || y != 0 || z != 0
      select new Vector3Int(x, y, z))
    .ToArray();

  IEnumerable<GridCube> GetNeighbors() {
    var world = CubeWorld.Instance;
    if (!world) {
      yield break;
    }

    foreach (var offset in NeighborOffsets) {
      var coord = cube.BoundingBox.GridCoord + offset;
      if (!world.TryGetCube(coord, out var neighbor)) {
        continue;
      }

      yield return neighbor;
    }
  }
}
}