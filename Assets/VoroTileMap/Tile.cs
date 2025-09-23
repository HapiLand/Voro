using UnityEngine;

namespace VoroTileMap {
public class Tile {
    public Tile(Vector2Int coords) {
        Coordinates = coords;
        Data = new TileData();
    }

    public Vector2Int Coordinates { get; private set; }
    public TileData Data { get; private set; }
}
}