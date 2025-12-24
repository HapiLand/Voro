using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using VoroSystem.Voro.Utilities.Extensions;
using VoroSystem.VoroWorldGeneration.CubicChunks.Player;
using VoroSystem.VoroWorldGeneration.CubicChunks.World;
using VoroSystem.VoroWorldGeneration.Map;

namespace VoroSystem.VoroWorldGeneration.CubicChunks.Cubes {
/// <summary>
/// represents a cube within a world grid, detects when player is located within
/// </summary>
[ExecuteAlways]
public class GridCube : MonoBehaviour {
  #region Serialized Fields
  public PlayerPoint player;
  public bool isPlayerInside;
  #endregion

  bool _lastIsPlayerInside;
  bool _neighborHasPlayer;
  public Vector3Int GridCoord { get; set; }
  Bounds CubeBounds => new(transform.position, Vector3.one * WorldSettings.GridSize);

  #region Event Functions
  void Awake() {
    player = PlayerLocator.GetOrCreatePlayer();
  }

  void Update() {
    UpdatePlayerDetection();
  }

  void OnDrawGizmos() {
    var cubeSize = WorldSettings.GridSize;

    if (isPlayerInside) {
      cubeSize *= 1f;
      Gizmos.color = Color.green;
    }
    else if (_neighborHasPlayer) {
      cubeSize *= 0.8f;
      Gizmos.color = Color.blue;
    }
    else {
      cubeSize = 0.25f;
      Gizmos.color = Color.red;
    }

    Gizmos.DrawWireCube(transform.position, Vector3.one * cubeSize);
  }
  #endregion

  void UpdatePlayerDetection() {
    if (!player) {
      isPlayerInside = false;
      return;
    }

    isPlayerInside = CubeBounds.Contains(player.transform.position);

    // Notify neighbors if player state changed
    if (isPlayerInside == _lastIsPlayerInside) {
      return;
    }

    _lastIsPlayerInside = isPlayerInside;
    NotifyNeighbors();
  }

  void NotifyNeighbors() {
    foreach (var neighbor in GetNeighbors()) {
      neighbor.OnNeighborPlayerInside(isPlayerInside);
    }
  }

  void OnNeighborPlayerInside(bool playerInside) {
    _neighborHasPlayer = playerInside;
  }

  IEnumerable<GridCube> GetNeighbors() {
    if (!CubeWorld.Instance) {
      yield break;
    }

    var offsets =
      from x in Enumerable.Range(-1, 3)
      from y in Enumerable.Range(-1, 3)
      from z in Enumerable.Range(-1, 3)
      where x != 0 || y != 0 || z != 0
      select new Vector3Int(x, y, z);

    foreach (var offset in offsets) {
      var neighborCoord = GridCoord + offset;

      // Skip if no cube exists at this coordinate
      if (!CubeWorld.Instance.IsValidCoord(neighborCoord)) {
        continue;
      }

      // Find the child cube at this coordinate
      foreach (Transform child in CubeWorld.Instance.transform) {
        var cube = child.GetComponent<GridCube>();
        if (cube != null && cube.GridCoord == neighborCoord) {
          yield return cube;
          break; // Found the neighbor, no need to keep searching
        }
      }
    }
  }

  public void GenerateWorldGrid() {

  }
}
}