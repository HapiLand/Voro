using UnityEngine;

namespace VoroSystem.Grids.Tiles {
public class TileCoordinate {
    public TileCoordinate(Vector2 position) {
        Position = position;
    }

    public Vector2 Position { get; }
}
}