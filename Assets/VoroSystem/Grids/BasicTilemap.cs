using System;
using VoroSystem.Grids.Tiles;

namespace VoroSystem.Grids {
/// <summary> Grid of Tiles </summary>
public class BasicTilemap : ITilemap<BasicTile> {
    readonly BasicTile[,] _tileGrid;

    public BasicTilemap(int tileSize, int sizeX, int sizeZ) {
        TileSize = tileSize;
        SizeX = sizeX;
        SizeZ = sizeZ;
        _tileGrid = new BasicTile[SizeX, SizeZ];
    }

    BasicTile this[int x, int y] {
        get => _tileGrid[x, y];
        set => _tileGrid[x, y] = value;
    }

    public int TileSize { get; protected set; }
    public int SizeX { get; protected set; }
    public int SizeZ { get; protected set; }

    public BasicTile GetTile(int x, int y) {
        return InBounds(x, y) ? this[x, y] : null;
    }

    public BasicTile GetTile(int index) {
        return GetTile(index % SizeX, index / SizeZ);
    }

    public void SetTile(int x, int y, BasicTile tile) {
        if (InBounds(x, y)) {
            this[x, y] = tile;
        }
    }

    public void SetTile(int index, BasicTile tile) {
        SetTile(index % SizeX, index / SizeZ, tile);
    }

    /// <summary> Tells if given coordinates are in bounds. </summary>
    public bool InBounds(int x, int y) {
        return x >= 0 && y >= 0 && x < SizeX && y < SizeZ;
    }

    public BasicTile GetTileUnsafe(int x, int y) {
        return this[x, y];
    }

    public BasicTile SetTileUnsafe(int x, int y, BasicTile tile) {
        return this[x, y] = tile;
    }

    public void ForEach(Action<BasicTile> action) {
        foreach (var t in _tileGrid) {
            action(t);
        }
    }


    /*public void UpdateVisibility(int padding) {
     var cam = CameraManager.Camera;
     var padX = (float)padding / SizeX;
     var padY = (float)padding / SizeY;

     for (var y = 0; y < SizeY; y++) {
         for (var x = 0; x < SizeX; x++) {
             var tile = GetTileUnsafe(x, y);
             var tileWorldPos = tile.Position.ToVector3();
             var viewportPos = cam.WorldToViewportPoint(tileWorldPos);
             var isVisible = viewportPos.z > 0 &&
                             viewportPos.x >= -padX && viewportPos.x <= 1 + padX &&
                             viewportPos.y >= -padY && viewportPos.y <= 1 + padY;

             tile.Visible = isVisible;
             SetTileUnsafe(x, y, tile);
         }
     }
 }*/
}
}