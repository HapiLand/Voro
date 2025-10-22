using System;
using UnityEngine;

namespace Voro.Core.Map {
public class TileMap : IMap<ITile> {
    readonly ITile[,] _tiles;
    int _xSize = 1;
    int _ySize = 1;

    public TileMap((int x, int y) mapSize) {
        XSize = mapSize.x;
        YSize = mapSize.y;
        Debug.Log($"[Tile Map] Constructing {Size.x}x{Size.y} Map");

        _tiles = new ITile[XSize, YSize];
        for (var y = 0; y < YSize; y++) {
            for (var x = 0; x < XSize; x++) {
                this[x, y] = new Tile(new Vector2(x, y));
            }
        }
    }

    int XSize {
        get => _xSize;
        set => _xSize = value < 1 ? 1 : value;
    }

    int YSize {
        get => _ySize;
        set => _ySize = value < 1 ? 1 : value;
    }

    public ITile this[int x, int y] {
        get => _tiles[x, y];
        set => _tiles[x, y] = value;
    }

    public ITile this[int index] => _tiles[index % _tiles.GetLength(0), index / _tiles.GetLength(1)];

    public (int x, int y) Size => (XSize, YSize);

    public void ForEach(Action<ITile> getTile) {
        for (var y = 0; y < YSize; y++) {
            for (var x = 0; x < XSize; x++) {
                getTile(_tiles[x, y]);
            }
        }
    }
}
}