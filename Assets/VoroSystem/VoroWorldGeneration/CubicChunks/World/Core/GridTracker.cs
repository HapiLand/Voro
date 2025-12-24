using UnityEngine;
using VoroSystem.VoroWorldGeneration.CubicChunks.Player;
using VoroSystem.VoroWorldGeneration.CubicChunks.Player.Core;

namespace VoroSystem.VoroWorldGeneration.CubicChunks.World.Core {
[ExecuteAlways]
public class GridTracker : MonoBehaviour {
  protected PlayerPoint Player { get; private set; }
  protected Vector3Int ActiveCoordinate { get; private set; }
  protected Vector3Int PreviousCoordinate { get; private set; }

  #region Event Functions
  protected virtual void Awake() {
    Player = PlayerLocator.GetOrCreatePlayer();
    ActiveCoordinate = PlayerLocator.GetPlayerGridCoordinate(Player, WorldSettings.GridSize);
    PreviousCoordinate = ActiveCoordinate;
  }

  protected virtual void Update() {
    if (TryUpdateCoordinate()) {
      OnCoordinateChanged(ActiveCoordinate);
    }
  }

  protected virtual void OnDrawGizmos() { }
  #endregion

  bool TryUpdateCoordinate() {
    var current =
      PlayerLocator.GetPlayerGridCoordinate(Player, WorldSettings.GridSize);

    if (current == ActiveCoordinate) {
      return false;
    }

    PreviousCoordinate = ActiveCoordinate;
    ActiveCoordinate = current;
    return true;
  }

  /// <summary>
  /// Called when the player moves into a new grid coordinate.
  /// </summary>
  protected virtual void OnCoordinateChanged(Vector3Int newCoord) { }
}
}