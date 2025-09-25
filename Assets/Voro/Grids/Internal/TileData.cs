namespace Voro.Grids.Internal {
public struct TileData {
    public static TileData CreateInstance(Coordinate coord) {
        return new TileData(coord);
    }

    public Coordinate Coordinate;

    TileData(Coordinate coord) {
        Coordinate = coord;
    }
}
}