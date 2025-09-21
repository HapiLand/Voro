using VoroUI.EditorTabs.Layers;
using VoroUI.EditorTabs.Nodes;
using VoroWorld.Diagrams;
using VoroWorld.Generation;
using VoroWorld.Generation.Effects.Base;
using VoroWorld.Grids;

namespace VoroGeneration {
/// <summary>
///     an object used to generate terrain
/// </summary>
public class Diagram {
    /// <summary>
    ///     voronoi point data for a tile
    /// </summary>
    readonly VoroDiagram _chunk;

    /// <summary>
    ///     the output from the shader
    /// </summary>
    readonly VoroResult _computeReturn;

    /// <summary>
    ///     user created layers.
    ///     for each generation function array
    /// </summary>
    readonly LayerInfo _computeThreadData;

    /// <summary>
    ///     user set values to drive generation
    /// </summary>
    readonly IControlData _controlElementData;

    /// <summary>
    ///     the seed object that produces the effect
    /// </summary>
    readonly INode _generatorDataRequirements;

    /// <summary>
    ///     produces a result via its function
    /// </summary>
    readonly IEffect _generatorFunction;

    /// <summary>
    ///     handle diagram events
    /// </summary>
    readonly DiagramManager _manager;

    /// <summary>
    ///     a game object entity that represents the tile
    /// </summary>
    readonly Tile _tile;

    public Diagram(VoroDiagram chunk, VoroResult computeReturn, LayerInfo computeThreadData,
        IControlData controlElementData, INode generatorDataRequirements, IEffect generatorFunction,
        DiagramManager manager, Tile tile) {
        _chunk = chunk;
        _computeReturn = computeReturn;
        _computeThreadData = computeThreadData;
        _controlElementData = controlElementData;
        _generatorDataRequirements = generatorDataRequirements;
        _generatorFunction = generatorFunction;
        _manager = manager;
        _tile = tile;
    }
}
}