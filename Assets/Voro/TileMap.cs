using System;

namespace Voro {
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
                var tile = new Tile(coord);
                _map[x, z] = tile;
                OnTileCreated?.Invoke(tile);
            }
        }

        OnMapGenerated?.Invoke();
    }

    public event Action OnMapGenerated;
    public event Action<Tile> OnTileCreated;

    public struct Coordinate {
        public readonly int X;
        public readonly int Z;

        public Coordinate(int x, int z) {
            X = x;
            Z = z;
        }
    }

    struct Dimensions {
        public readonly int XSize;
        public readonly int ZSize;

        public Dimensions(int x, int z) {
            XSize = x;
            ZSize = z;
        }
    }
}
}