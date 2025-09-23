namespace Voro {
public class Tile {
    TileMap.Coordinate _coordinate;
    TileData _metadata;

    public Tile(TileMap.Coordinate coord) {
        _coordinate = coord;
        _metadata = new TileData();
    }

    struct TileData { }
}
}