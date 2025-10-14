using System.Collections.Generic;
using VoroSystem.GraphEditor.Effects;
using VoroSystem.GraphEditor.Output;
using VoroSystem.Terrain.Generation.PostCompute;
using VoroSystem.WorldGrid.Grids;

namespace VoroSystem.Terrain.Generation.PreCompute {
/// <summary> Diagram stores Tiles and Graphs -> to Compute -> make Result </summary>
public interface IDiagram {
    /*/// <summary> collection of all Tiles in the World Map </summary>
    IReadOnlyList<ITile> Tiles { get; }

    /// <summary> collection of all Terrain Generation Graphs </summary>
    IReadOnlyList<IGraph> Graphs { get; }

    /// <summary> executes the Terrain Generation Process and produces a Generation Result </summary>
    IResult Compute(ITile tile, IEffect effect);*/
}
}