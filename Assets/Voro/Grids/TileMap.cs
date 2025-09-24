using System;

namespace Voro.Grids {
/// <summary>
///     - Stores positional data for all Chunks.
///     - Serves as the environment map to be placed into the VoroWorld
/// </summary>
public class TileMap {
    readonly Tile[,] _map;
    readonly Dimensions _mapSize;

    public TileMap() {
        _mapSize = new Dimensions(10, 10);
        _map = new Tile[_mapSize.XSize, _mapSize.ZSize];
    }

    public void GenerateMap() {
        for (var x = 0; x < _mapSize.XSize; x++) {
            for (var z = 0; z < _mapSize.ZSize; z++) {
                var coord = new Coordinate(x, z);
                var tile = CreateTile(coord);
                SetTile(x, z, tile);
            }
        }

        OnMapGenerated?.Invoke();
    }

    Tile CreateTile(Coordinate coord) {
        var tile = new Tile(coord);
        return tile;
    }

    void SetTile(int x, int z, Tile tile) {
        _map[x, z] = tile;
        OnTileSet?.Invoke(tile);
    }


    public event Action OnMapGenerated;
    public event Action<Tile> OnTileSet;


}
}