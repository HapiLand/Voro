using System;
using System.Collections.Generic;
using UnityEngine;

namespace Voro.Internal.World.PlayerOrigins {
[ExecuteAlways]
public class PlayerOriginMonitor : MonoBehaviour {
  readonly HashSet<PlayerOrigin> _players = new();
  public static PlayerOriginMonitor Instance { get; private set; }
  public IReadOnlyCollection<PlayerOrigin> PlayerOrigins => _players;

  #region Event Functions
  void Awake() {
    if (Instance != null) {
      Destroy(gameObject);
      return;
    }

    Instance = this;
  }
  #endregion

  public event Action<PlayerOrigin> PlayerRegistered;
  public event Action<PlayerOrigin> PlayerUnregistered;

  public void Register(PlayerOrigin player) {
    if (player == null) {
      return;
    }

    if (_players.Add(player)) {
      PlayerRegistered?.Invoke(player);
    }
  }

  public void Unregister(PlayerOrigin player) {
    if (player == null) {
      return;
    }

    if (_players.Remove(player)) {
      PlayerUnregistered?.Invoke(player);
    }
  }
}
}