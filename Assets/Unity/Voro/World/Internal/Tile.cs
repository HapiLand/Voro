using UnityEngine;

namespace Voro.World.Internal {
class Tile {
    readonly TileData _data;

    public Tile(Coordinate coord) {
        _data = TileData.CreateInstance(coord);
    }

    Coordinate Coord => _data.Coordinate;
    public Vector3 TilePosition => Coord.WorldPosition();
}
}