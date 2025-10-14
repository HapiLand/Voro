using UnityEngine;
using VoroSystem.GraphEditor;
using VoroSystem.Terrain.Generation.Compute;
using VoroSystem.Terrain.Generation.PostCompute;
using VoroSystem.Terrain.Generation.PreCompute;
using VoroSystem.Terrain.World;
using VoroSystem.WorldGrid;
using VoroSystem.WorldGrid.Grids;

namespace VoroSystem.Terrain.Overseer {
[ExecuteAlways]
public class WorldGenerationOverseer : MonoBehaviour, IWorldGenerationOverseer {
    /*IDesigner _graphDesigner;
    IWorld _worldMap;

    void Awake() {
        if (!SetupComponents()) {
            Debug.LogError("Generator lacks either component WorldMap or GraphDesigner");
            return;
        }

        Initialize();
        return;

        void Initialize() {
            Debug.Log("Initialize WorldGenerationOverseer");

            // create a new Diagram holding the Map and Graphs
            Diagram = new Diagram(_worldMap, _graphDesigner);
            Debug.Log("new Diagram created");

            // assign the terrain generator instance
            Generator = new TerrainGenerator();
            Debug.Log("new TerrainGenerator created");
        }
    }

    public IDiagram Diagram { get; private set; }
    public IGenerator Generator { get; private set; }
    public IResult Result { get; set; }

    /// <summary> Generate the entire World Terrain and builds the Result </summary>
    public void GenerateWorld() {
        Debug.Log("Generating world");

        if (Diagram == null || Generator == null) {
            Debug.LogError("Generator lacks either Diagram or Generator");
            return;
        }

        // generate the full world terrain from the Diagram, produces the computed result
        Result = Generator.GenerateComputedResultForFullWorld(Diagram);

        // build the Result for the full world, which produces the Landscape
        ResultToWorldBuilderFactory(Result);
    }

    /// <summary> converts the Generation Result to the actual Scene Landscape </summary>
    public void ResultToWorldBuilderFactory(IResult result) {
        if (result == null) {
            Debug.LogError("WorldGenerationOverseer.Result does not exist, cannot build");
            return;
        }

        // iterate through each Tile to build its Mesh information
        foreach (var (tile, meshData) in result.TileMeshes) {
            // converts the mesh data into the ground surface where the Tile is located
            WorldBuilderFactory.BuildGroundMesh(tile, meshData);

            // place every object in the Tile into the world
            tile.InstantiateTile(transform);
        }
    }

    bool SetupComponents() {
        if (!GetComponent<GraphDesigner>()) {
            Debug.LogError("GraphDesigner not found");
            return false;
        }

        _graphDesigner = GetComponent<GraphDesigner>();

        if (!GetComponent<WorldMap>()) {
            Debug.LogError("WorldMap not found");
            return false;
        }

        _worldMap = GetComponent<WorldMap>();

        return true;
    }


    /// <summary>
    ///     For a single Tile, Generate its World Terrain and build the Result.
    ///     the Tiles Result is combined with the other Results, appending the new value.
    ///     Used to update the pre-existing Result associated with that Tile.
    /// </summary>
    public void GenerateTile(ITile tile) {
        if (Diagram == null || Generator == null || tile == null) {
            return;
        }

        // make sure the Tile has not yet been generated
        if (!Generator.DoesTileNeedGenerating(tile)) {
            return;
        }

        // generate the world terrain for this Tile, produces its computed Result
        var tileResult = Generator.GeneratePartialComputedResultForATile(Diagram, tile);
        // combine the Result with the others
        Result = Result.Combine(tileResult);

        // build the Result only for this Tile to show the Landscape only for the Tile
        ResultToWorldBuilderFactory(tileResult);
    }*/
}
}