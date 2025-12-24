using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using VoroSystem.VoroWorldGeneration.CubicChunks.Player;
using VoroSystem.VoroWorldGeneration.CubicChunks.World;

namespace VoroSystem.VoroWorldGeneration.CubicChunks.Cubes.Core {
public class CubePlayerDetection {
  static readonly Vector3Int[] NeighborOffsets = GenerateNeighborOffsets();
  readonly GridCube _cube;
  bool _lastPlayerInside;


  public CubePlayerDetection(GridCube cube) {
    _cube = cube;
  }

  public bool IsPlayerInside { get; private set; }
  public bool NeighborHasPlayer { get; private set; }
  public PlayerPoint Player { get; set; }

  public void Update() {
    if (!Player) {
      SetPlayerInside(false);
      return;
    }

    var inside = _cube.BoundingBox.Bounds.Contains(Player.transform.position);
    SetPlayerInside(inside);
  }

  void SetPlayerInside(bool inside) {
    if (inside == _lastPlayerInside) {
      return;
    }

    IsPlayerInside = inside;
    _lastPlayerInside = inside;
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


  static Vector3Int[] GenerateNeighborOffsets() {
    return
      (from x in Enumerable.Range(-1, 3)
        from y in Enumerable.Range(-1, 3)
        from z in Enumerable.Range(-1, 3)
        where x != 0 || y != 0 || z != 0
        select new Vector3Int(x, y, z))
      .ToArray();
  }

  IEnumerable<GridCube> GetNeighbors() {
    var world = CubeWorld.Instance;
    if (!world) {
      yield break;
    }

    foreach (var offset in NeighborOffsets) {
      var coord = _cube.BoundingBox.GridCoord + offset;
      if (!world.TryGetCube(coord, out var neighbor)) {
        //if (!CubeWorld.Instance.IsValidCoord(neighborCoord)) {
        continue;
      }

      yield return neighbor;
    }
  }
}
}