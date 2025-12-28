using UnityEngine;
using Voro.Internal.World.GridTiles;

namespace Voro.Internal.World.WorldChunks {
/// <summary>
/// bounding box component
/// </summary>
public class WorldChunkBounds : MonoBehaviour {
  GridTile _gridTile;
  Vector3Int GridCoordinate => _gridTile.Coordinate;
  Bounds Bounds => new(transform.position, Vector3.one * GridTileSettings.GridTileSize);

  public Vector3 WorldOriginPosition => new Vector3(GridCoordinate.x, GridCoordinate.y, GridCoordinate.z) *
                                        GridTileSettings.GridTileSize;

  public Vector3Int BoundSize =>
    new(CeilToIntMin1(Bounds.size.x), CeilToIntMin1(Bounds.size.y), CeilToIntMin1(Bounds.size.z));

  static int CeilToIntMin1(float value) {
    return Mathf.Max(1, Mathf.CeilToInt(value));
  }
}
}