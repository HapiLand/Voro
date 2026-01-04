using UnityEngine;

namespace Voro.Internal.World.GameWorldMap.WorldTiles {
/// <summary>
/// a tile for the world grid
/// </summary>
[ExecuteAlways]
public class WorldTile : MonoBehaviour {
  #region Serialized Fields
  public Vector3Int coordinate;
  public bool isActive;
  #endregion

  #region Event Functions
  void LateUpdate() {
    UpdateVisibility(out var visible);
    isActive = visible;
  }


  void OnDrawGizmos() {
    Gizmos.color = Color.white;
    var pos = new Vector3(coordinate.x, 0, coordinate.z);
    var size = new Vector3(TileSettings.TileSize, 0f, TileSettings.TileSize);

    Gizmos.DrawWireCube(pos, size);
    if (isActive) {
      Gizmos.color *= new Color(1f, 1f, 1f, 0.1f);
      Gizmos.DrawCube(pos, size);
    }
  }
  #endregion

  void UpdateVisibility(out bool b) {
    var worldPos = new Vector3(coordinate.x, 0, coordinate.z);
    var viewportPos = Camera.main.WorldToViewportPoint(worldPos);
    b = viewportPos is { z: > 0, x: >= 0 and <= 1, y: >= 0 and <= 1 };
  }
}
}