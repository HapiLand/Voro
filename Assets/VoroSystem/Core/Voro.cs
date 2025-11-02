using VoroSystem.Grids;
using VoroSystem.TilemapSystem;
using VoroSystem.World.Generator;

namespace VoroSystem.Core {
public class Voro {
    /// <summary> VoroInput holds the initial state to start the system with </summary>
    readonly VoroInput _input;

    VoroLandscapeGenerator _generator;
    BasicTilemapComponent _tilemapComponent;

    public Voro(VoroInput voroInput) {
        _input = voroInput;
    }

    /// <summary> Initialise the Voro System </summary>
    public void Init() {
        _tilemapComponent = MapBuilder.CreateBasicTilemapComponent(_input);
        _generator = new VoroLandscapeGenerator(_tilemapComponent);
    }


    /// <summary> Generate the 3D landscape </summary>
    public void CreateLandscape() {
        var graph = _generator.LandscapeGraph.BuildGraph();
        _generator.LandscapeCompute.Compute(graph);
        _generator.LandscapeMesh.GenerateMesh();
    }
}
}