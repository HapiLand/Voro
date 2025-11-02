using VoroSystem.Grids;

namespace VoroSystem.TilemapSystem {
public class CompMap {
    MapBuilder _mapBuilder;
    public (int x, int z) Bounds;

    /// <summary> Component is responsible for building the Tilemap </summary>
    public BasicTilemap Tilemap;

    public MapBuilder TilemapBuilder {
        get => _mapBuilder;
        set
        {
            if (_mapBuilder != null) {
                // builder already set
                return;
            }

            // builder set for the first time
            _mapBuilder = value;
            Tilemap = _mapBuilder.Build(Bounds);
        }
    }
}
}