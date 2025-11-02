using System;
using VoroSystem.Landscape.World;

namespace VoroSystem.Landscape.Tilemap {
class WorldGrid : ITilemap<Tile> {
    bool _visible;

    /// <summary>
    /// number of tiles drawn outside camera visibility
    /// </summary>
    public int Padding = 0;

    public WorldGrid(float tileSize) {
        _visible = true;
        TileSize = tileSize;
        Width = (int)VoroWorld.WorldBounds.Size.x;
        Height = (int)VoroWorld.WorldBounds.Size.y;
        Tiles = new Tile[Width, Height];
    }

    public Tile[,] Tiles { get; }

    public void SetTile(int x, int y, Tile tile) {
        if (InBounds(x, y)) {
            Tiles[x, y] = tile;
        }
    }

    /// <summary>
    /// Tells if given coordinates are in bounds.
    /// </summary>
    public bool InBounds(int x, int y) {
        return x >= 0 && y >= 0 && x < Width && y < Height;
    }

    public int Width { get; }
    public int Height { get; }
    public float TileSize { get; }

    public Tile? GetTile(int x, int y) {
        if (InBounds(x, y)) {
            return Tiles[x, y];
        }

        return null;
    }

    public void ForEachTile(Action<Tile> getTile) {
        for (var y = 0; y < Width; y++) {
            for (var x = 0; x < Height; x++) {
                getTile(Tiles[x, y]);
            }
        }
    }
}
}