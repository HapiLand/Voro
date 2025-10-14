using System;
using System.Collections.Generic;
using UnityEngine;
using VoroSystem.Terrain.Generation.PostCompute;
using VoroSystem.Terrain.Generation.PreCompute;
using VoroSystem.WorldGrid.Grids;

namespace VoroSystem.Terrain.Generation.Compute {
public class TerrainGenerator : IGenerator {
    /*/// <summary>
    ///     turn the entire Diagram into the full world terrain
    /// </summary>
    /// <param name="diagram"> the diagram that says what the world should generate as </param>
    /// <returns> the fully computed result </returns>
    public IResult GenerateComputedResultForFullWorld(IDiagram diagram) {
        var tiles = diagram.Tiles;
        var graphs = diagram.Graphs;
        Debug.Log($"Loaded '{tiles.Count}' Tiles and '{graphs.Count}' Graphs");
        if (tiles.Count == 0) {
            Debug.LogError("At least 1 Tile is required to compute");
            return null;
        }

        if (graphs.Count == 0) {
            Debug.LogError("At least 1 Graph is required to compute");
            return null;
        }

        // create a new Result that shall contain the fully generated world
        // is an overall accumulation, each Tile that gets computed adds itself into this
        var fullResult = Result.CreateEmptyInstance();

        var counter = 0;
        // accumulate all Tile Results into the full world Result
        foreach (var tileResult in EnumerateWorldResults()) {
            fullResult.Combine(tileResult);
            counter++;
        }

        // return the result that has the fully generated world terrain
        Debug.Log($"Final full world Result stores the Result of '{counter}' Tiles");
        return fullResult;

        // iterator to yield an accumulated Result for every Tile
        IEnumerable<IResult> EnumerateWorldResults() {
            foreach (var tile in tiles) {
                var coord = $"'{tile.Coord.x} x {tile.Coord.y}'";
                var counter = 0;
                Debug.Log($"Producing new Result for Tile {coord}");

                // make a Result to store the Tile generation
                // this accumulates the Result every time an Effect is computed
                var tileResult = Result.CreateTileResult(tile);

                // dispatch each Effect where an existing Tile Result is computed
                // this accumulates every Effect into the Result
                foreach (var partial in EnumerateTileResults(tile)) {
                    // accumulate the Result this Effect computed on the Tile
                    tileResult.Combine(partial);
                    Debug.Log($"[{counter}] Accumulate the computed Result with the existing Result for Tile {coord}");
                    counter++;
                }

                // return the final accumulated Result from computing every Effect on the Tile
                Debug.Log($"Accumulated a total of '{counter}' Results for Tile {coord}");
                yield return tileResult;
            }
        }

        // iterator to yield a Result for every Effect on a specific Tile
        IEnumerable<IResult> EnumerateTileResults(ITile tile) {
            // for every Graph compute each Effect
            foreach (var graph in graphs) {
                Debug.Log($"Current Graph = '{graph.Name}' contains '{graph.Effects.Count}' Effects");
                foreach (var effect in graph.Effects) {
                    // call the Compute method so that the Effect can compute a Result for the Tile
                    Debug.Log($"Current Effect = '{effect.Name}'");
                    yield return diagram.Compute(tile, effect);
                }
            }
        }
    }

    /// <summary>
    ///     turn a single Tile in the Diagram into world terrain thats only found in the Tile
    /// </summary>
    /// <param name="diagram"> the diagram that says what the world should generate as </param>
    /// <param name="tile"> the tile to compute </param>
    /// <returns> the fully computed tiles result </returns>
    public IResult GeneratePartialComputedResultForATile(IDiagram diagram, ITile tile) {
        throw new NotImplementedException();
    }

    /// <summary>
    ///     determine whether this Tile needs to be generated or if it already is up to date
    /// </summary>
    /// <param name="tile"> the tile to check </param>
    /// <returns> true when the tile has not been generated </returns>
    public bool DoesTileNeedGenerating(ITile tile) {
        throw new NotImplementedException();
    }*/
}
}