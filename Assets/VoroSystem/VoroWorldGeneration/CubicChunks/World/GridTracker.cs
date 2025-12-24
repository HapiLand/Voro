using System;
using UnityEngine;
using VoroSystem.VoroWorldGeneration.CubicChunks.Player;

namespace VoroSystem.VoroWorldGeneration.CubicChunks.World {
[ExecuteAlways]
public class GridTracker : MonoBehaviour {
  #region Serialized Fields
  public PlayerPoint player;
  public Vector3Int activeCoordinate;
  public Vector3Int previousCoordinate;
  #endregion

  #region Event Functions
  protected virtual void Awake() {
    player = PlayerLocator.GetOrCreatePlayer();
    activeCoordinate = PlayerLocator.GetPlayerGridCoordinate(player, WorldSettings.GridSize);
    previousCoordinate = activeCoordinate;
  }

  protected virtual void Update() {
    if (UpdateCoordinate()) {
      OnCoordinateChanged(activeCoordinate);
    }
  }
  
  protected virtual void OnDrawGizmos() { }
  #endregion

  bool UpdateCoordinate() {
    var currentCoordinate = PlayerLocator.GetPlayerGridCoordinate(player, WorldSettings.GridSize);
    if (currentCoordinate == activeCoordinate) {
      return false;
    }

    previousCoordinate = activeCoordinate;
    activeCoordinate = currentCoordinate;
    return true;
  }

  /// <summary>
  /// when the player moves into a new coordinate3
  /// </summary>
  /// <param name="newCoord"> </param>
  protected virtual void OnCoordinateChanged(Vector3Int newCoord) { }
}
}