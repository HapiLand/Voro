using UnityEngine;

namespace Voro.Internal.World.GridTiles {
/// <summary>
/// debug draw to show every registered tile
/// </summary>
[ExecuteAlways]
public class GridTileGizmos : MonoBehaviour {
  #region Event Functions
  void OnDrawGizmos() {
    foreach (var tile in GridTileMonitor.Tiles) {
      // todo draw each tile as a square
    }
  }
  #endregion
}
}