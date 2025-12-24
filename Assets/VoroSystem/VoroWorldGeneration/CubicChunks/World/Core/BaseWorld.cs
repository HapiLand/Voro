using UnityEngine;
using VoroSystem.VoroWorldGeneration.CubicChunks.World.Core.States;

namespace VoroSystem.VoroWorldGeneration.CubicChunks.World.Core {
[ExecuteAlways]
public abstract class BaseWorld : MonoBehaviour {
  #region Serialized Fields
  public WorldState worldState;

  [field: SerializeField] protected GridTracker GridTracker { get; private set; }
  #endregion

  #region Event Functions
  protected virtual void Awake() {
    GridTracker = new GridTracker();
  }

  protected virtual void Update() {
    // check if the player has moved from one cube to a different cube
    if (GridTracker.TryUpdateCoordinate()) {
      OnCoordinateChanged(GridTracker.ActiveCoordinate);
    }
  }
  #endregion

  protected virtual void OnCoordinateChanged(Vector3Int newCoord) { }
}
}