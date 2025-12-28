using UnityEditor;
using UnityEngine;
using Voro.Internal.World.GridTiles;
using Voro.Internal.World.PlayerOrigins;

namespace Voro.Internal.World {
/// <summary>
/// manages the game world with the grid and chunks
/// </summary>
[ExecuteAlways]
[RequireComponent(typeof(PlayerOriginMonitor))]
[RequireComponent(typeof(GridTileGizmos))]
[RequireComponent(typeof(ChunkGizmos))]
public class GameWorldManager : MonoBehaviour {
  GridTileFactory _gridTileFactory;
  PlayerOriginMonitor _playerOrigins;

  #region Event Functions
  void Awake() {
    _playerOrigins = GetComponent<PlayerOriginMonitor>();
    _gridTileFactory = new GridTileFactory();

    _playerOrigins.PlayerRegistered += OnPlayerRegistered;
    _playerOrigins.PlayerUnregistered += OnPlayerUnregistered;
    foreach (var player in _playerOrigins.PlayerOrigins) {
      if (player != null) {
        OnPlayerRegistered(player);
      }
    }
  }

  void OnDisable() {
    _playerOrigins.PlayerRegistered -= OnPlayerRegistered;
    _playerOrigins.PlayerUnregistered -= OnPlayerUnregistered;
  }

  void OnDrawGizmos() {
    if (_playerOrigins == null) {
      return;
    }

    Gizmos.color = Color.cyan;
    foreach (var player in _playerOrigins.PlayerOrigins) {
      if (player == null) {
        continue;
      }

      var t = player.transform;

      Gizmos.DrawSphere(t.position, 0.25f);
      Gizmos.DrawLine(t.position, t.position + t.forward);
    }
  }
  #endregion

  void OnPlayerRegistered(PlayerOrigin player) {
    // create a grid tile at the playe
    _gridTileFactory.AddPlayerOrigin(player);
  }

  void OnPlayerUnregistered(PlayerOrigin player) { }

  [MenuItem("Voro/World Manager")]
  static void CreateWorldManager() {
    if (FindFirstObjectByType<GameWorldManager>() != null) {
      return;
    }

    var go = new GameObject("GameWorldManager");
    go.AddComponent<GameWorldManager>();
  }
}
}