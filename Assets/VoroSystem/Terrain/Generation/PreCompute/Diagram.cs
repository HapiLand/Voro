using System.Collections.Generic;
using UnityEngine;
using VoroSystem.GraphEditor;
using VoroSystem.GraphEditor.Effects;
using VoroSystem.GraphEditor.Output;
using VoroSystem.Terrain.Generation.PostCompute;
using VoroSystem.WorldGrid;
using VoroSystem.WorldGrid.Grids;

namespace VoroSystem.Terrain.Generation.PreCompute {
public class Diagram : IDiagram {
    /*public Diagram(IWorld worldMap, IDesigner graphDesigner) {
        // get every Tile within the World Map
        var tiles = new List<ITile>();
        worldMap.ForEach(tile => tiles.Add(tile));
        Tiles = tiles;
        Debug.Log($"Set {tiles.Count} Tiles in Diagram");

        // get every Graph within the Designer
        var graphs = new List<IGraph>();
        graphDesigner.ForEachGraph(graph => graphs.Add(graph));
        Graphs = graphs;
        Debug.Log($"Set {graphs.Count} Graphs in Diagram");
    }

    public IReadOnlyList<ITile> Tiles { get; }
    public IReadOnlyList<IGraph> Graphs { get; }

    /// <summary>
    ///     dispatches the Effect to produce a Result.
    ///     the Effect finds the elevation of every Point within the Tile
    /// </summary>
    /// <param name="tile"> Tile that the heights are being computed for </param>
    /// <param name="effect"> Effect that shall carry out its shader code </param>
    /// <returns> a Result that stores the Tile where the Tile has been given elevation </returns>
    public IResult Compute(ITile tile, IEffect effect) {
        var coord = $"'{tile.Coord.x} x {tile.Coord.y}'";
        Debug.Log($"Effect '{effect.Name}' to compute Result for on Tile '{coord}'");

        // dispatch the Shader by passing the Tile into it
        // the Shader will generate elevation values that are written to a new Result
        var result = effect.Dispatch(tile);
        return result;
    }*/
}
}