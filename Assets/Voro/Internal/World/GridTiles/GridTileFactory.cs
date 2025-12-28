using System.Collections.Generic;
using Voro.Internal.World.PlayerOrigins;

namespace Voro.Internal.World.GridTiles {
/// <summary>
/// creates a grid tile instance, registers the tile
/// </summary>
public class GridTileFactory {
  /// <summary>
  /// grid tiles are created at the player origins
  /// </summary>
  HashSet<PlayerOrigin> _players = new();
  /// <summary>
  /// store the player origin in the factory
  /// </summary>
  /// <param name="origin"></param>
  public void AddPlayerOrigin(PlayerOrigin origin) {
    _players.Add(origin);
  }
}
}