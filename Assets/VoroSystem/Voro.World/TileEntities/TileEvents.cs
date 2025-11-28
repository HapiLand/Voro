using System;
using VoroSystem.Voro.World.Map;

namespace VoroSystem.Voro.World.TileEntities {
public static class TileEvents {
  public static event Action<Tile> TileCreated;

  public static void RaiseTileCreated(Tile tile) {
    TileCreated?.Invoke(tile);
  }
}
}