using System;

namespace Voro.Grids.Internal {
public struct TileData {
    public static TileData CreateInstance(Coordinate coord) {
        return new TileData(coord);
    }

    public Coordinate Coordinate;
    public int ID;
    public string[] Layers;

    TileData(Coordinate coord) {
        Coordinate = coord;
        ID = -404;
        Layers = Array.Empty<string>();
    }
}
}