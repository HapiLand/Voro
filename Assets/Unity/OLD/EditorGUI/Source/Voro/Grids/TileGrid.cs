using UnityEngine;

namespace EditorGUI.Source.Voro.Grids {
/// <summary>
///     handles the creation of a 2D grid of WorldTiles
///     handles only the layout of the tiles that exist in the world
///     TileGrid shall define the dimensions of a limited size map, not infinitely generating
/// </summary>
public class TileGrid {
    public WorldTile[,] Tiles;

    public TileGrid(int width, int height, Transform parentTransform) {
        Debug.Log("new TileGrid");

        Dimensions = (width, height);
        Tiles = new WorldTile[width, height];
        for (var x = 0; x < width; x++) {
            for (var z = 0; z < height; z++) {
                var tile = new WorldTile(x, z);
                tile.TileContainer.transform.SetParent(parentTransform);
                Tiles[x, z] = tile;
            }
        }
    }

    public (int width, int height) Dimensions { get; }

    public WorldTile GetTile(int x, int z) {
        if (x < 0 || z < 0 || x >= Dimensions.width || z >= Dimensions.height) {
            return null;
        }

        return Tiles[x, z];
    }
}
}