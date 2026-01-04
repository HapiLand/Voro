using System.Collections.Generic;

namespace Voro.Internal.World {
/// <summary>
/// contains and monitors the world grid
/// </summary>
public static class GridTileMonitor {
  static readonly HashSet<GridTile> _tiles = new();
  public static IReadOnlyCollection<GridTile> Tiles => _tiles;

  /// <summary> grid tile that the player is inside </summary>
  public static GridTile ActiveGridTile => null;

  /// <summary> the last grid tile which was active </summary>
  public static GridTile LastActiveGridTile => null;

  public static void Register(GridTile tile) {
    if (tile != null) {
      _tiles.Add(tile);
    }
  }

  public static void Unregister(GridTile tile) {
    if (tile != null) {
      _tiles.Remove(tile);
    }
  }
}
}