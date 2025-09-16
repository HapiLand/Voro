using UnityEngine;

namespace EditorGUI.Source.Voro.Grids {
public class TileGrid {
    readonly WorldTile[,] _tiles;

    public TileGrid(int width, int height, Transform parentTransform) {
        Dimensions = (width, height);
        _tiles = new WorldTile[width, height];

        for (var x = 0; x < width; x++) {
            for (var z = 0; z < height; z++) {
                var tile = new WorldTile(x, z);
                tile.TileContainer.transform.SetParent(parentTransform);
                _tiles[x, z] = tile;
            }
        }
    }

    public (int width, int height) Dimensions { get; }

    public WorldTile GetTile(int x, int z) {
        if (x < 0 || z < 0 || x >= Dimensions.width || z >= Dimensions.height) {
            return null;
        }

        return _tiles[x, z];
    }
}
}