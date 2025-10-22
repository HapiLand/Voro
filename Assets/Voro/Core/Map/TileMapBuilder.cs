using UnityEngine;
using Voro.Core.Bounds;

namespace Voro.Core.Map {
/// <summary> Concrete builder that produces tiles within the region of a Landscape </summary>
class TileMapBuilder : IMapBuilder {
    readonly Landscape _landscape;
    public TileMap TileMap;

    public TileMapBuilder(Landscape landscape) {
        _landscape = landscape;
    }

    /// <summary> Size of the Map array </summary>
    (int x, int y) Size => _landscape.GridSize;

    /// <summary> Builds the 2D array that represents this Map </summary>
    public void Build() {
        Debug.Log("[Tile Map Builder] Building Tile Map");
        TileMap = new TileMap(Size);
    }
}
}