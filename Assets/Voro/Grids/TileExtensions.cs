using System.Collections.Generic;

namespace Voro.Grids {
public static class TileExtensions {
    public static IEnumerable<Tile> AsEnumerable(this TileMap tiles) {
        var xLength = tiles.Map.GetLength(0);
        var zLength = tiles.Map.GetLength(1);

        for (var x = 0; x < xLength; x++) {
            for (var z = 0; z < zLength; z++) {
                yield return tiles.GetTile(x, z);
            }
        }
    }
}
}