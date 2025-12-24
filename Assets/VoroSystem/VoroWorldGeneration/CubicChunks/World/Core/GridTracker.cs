using System;
using UnityEngine;
using VoroSystem.VoroWorldGeneration.CubicChunks.Player;
using VoroSystem.VoroWorldGeneration.CubicChunks.Player.Core;

namespace VoroSystem.VoroWorldGeneration.CubicChunks.World.Core {
[Serializable]
public class GridTracker {
  #region Serialized Fields
  public PlayerPoint player;
  [field: SerializeField] public Vector3Int ActiveCoordinate { get; private set; }
  [field: SerializeField] public Vector3Int PreviousCoordinate { get; private set; }
  #endregion

  public GridTracker() {
    player = PlayerLocator.GetOrCreatePlayer();
    ActiveCoordinate = PlayerLocator.GetPlayerGridCoordinate(player, GridSize);
    PreviousCoordinate = ActiveCoordinate;
  }

  float GridSize => WorldSettings.GridSize;

  public bool TryUpdateCoordinate() {
    var current = PlayerLocator.GetPlayerGridCoordinate(player, GridSize);
    if (current == ActiveCoordinate) {
      return false;
    }

    PreviousCoordinate = ActiveCoordinate;
    ActiveCoordinate = current;
    return true;
  }
}
}