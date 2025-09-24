using Voro.Grids.Internal;

namespace Voro.Grids {
public class Tile {
    readonly TileData _metadata;

    public Tile(Coordinate coord) {
        _metadata = TileData.CreateInstance(coord);
    }

    public Coordinate Coordinate => _metadata.Coordinate;
}
}