using System.Collections.Generic;

namespace Voro.World.Internal {
class TileMap {
    public readonly Tile[,] Map;
    public readonly Dimension MapSize;

    public TileMap(int xSize, int ySize) {
        MapSize = new Dimension(xSize, ySize);
        Map = new Tile[MapSize.XSize, MapSize.ZSize];

        for (var x = 0; x < MapSize.XSize; x++) {
            for (var z = 0; z < MapSize.ZSize; z++) {
                var coord = new Coordinate(x, z);
                var tile = CreateTile(coord);
                SetTile(x, z, tile);
            }
        }
    }

    public IEnumerable<Tile> AsEnumerable() {
        var xLength = Map.GetLength(0);
        var zLength = Map.GetLength(1);

        for (var x = 0; x < xLength; x++) {
            for (var z = 0; z < zLength; z++) {
                yield return GetTile(x, z);
            }
        }
    }

    Tile CreateTile(Coordinate coord) {
        var tile = new Tile(coord);
        return tile;
    }

    void SetTile(int x, int z, Tile tile) {
        Map[x, z] = tile;
    }

    public Tile GetTile(int x, int z) {
        return Map[x, z];
    }
}
}