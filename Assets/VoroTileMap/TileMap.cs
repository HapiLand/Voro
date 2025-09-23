using System;
using UnityEngine;

namespace VoroTileMap {
public class TileMap {
    readonly int _length;
    readonly int _width;

    public TileMap(int width, int length) {
        _width = width;
        _length = length;
        TilesArray = new Tile[width, length];
    }

    public Tile[,] TilesArray { get; }
    public event Action<Tile> OnTileCreated;
    public event Action OnTileArrayCompletedGeneration;

    public void GenerateTiles() {
        Debug.Log("Generating Tiles in TileMap");

        // generate every tile in the map
        for (var x = 0; x < _width; x++) {
            for (var y = 0; y < _length; y++) {
                var tile = new Tile(new Vector2Int(x, y));
                TilesArray[x, y] = tile;
                OnTileCreated?.Invoke(tile);
            }
        }

        // the Diagram is created now that the TileMap is done
        OnTileArrayCompletedGeneration?.Invoke();
    }
}
}