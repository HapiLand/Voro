using VoroSystem.TilemapSystem;
using VoroSystem.World.Generate;

namespace VoroSystem.World.Generator {
class VoroLandscapeGenerator {
    public readonly BasicTilemapComponent TilemapComponent;
    public VoroTerrain ComputedVoroTerrain;

    public VoroLandscapeGenerator(BasicTilemapComponent tilemapComponent) {
        TilemapComponent = tilemapComponent;
        LandscapeGraph = new LandscapeGraph();
        LandscapeCompute = new LandscapeCompute(this);
        LandscapeMesh = new LandscapeMesh(this);
    }

    public LandscapeGraph LandscapeGraph { get; }

    public LandscapeCompute LandscapeCompute { get; }

    public LandscapeMesh LandscapeMesh { get; }
}
}