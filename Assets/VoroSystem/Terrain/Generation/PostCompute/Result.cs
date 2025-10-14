using System;
using System.Collections.Generic;
using UnityEngine;
using VoroSystem.WorldGrid.Coordinate;
using VoroSystem.WorldGrid.Grids;

namespace VoroSystem.Terrain.Generation.PostCompute {
public class Result : IResult {
    /*Result(IReadOnlyDictionary<Vector2Int, ITile> tiles, IReadOnlyDictionary<ITile, ImmutableMeshData> tileMeshes) {
        Tiles = tiles;
        TileMeshes = tileMeshes;
    }

    public IReadOnlyDictionary<Vector2Int, ITile> Tiles { get; }
    public IReadOnlyDictionary<ITile, ImmutableMeshData> TileMeshes { get; }

    public IResult Combine(IResult other) {
        var combinedTiles = new Dictionary<Vector2Int, ITile>(Tiles);

        foreach (var kvp in combinedTiles) {
            var coord = kvp.Key;
            var otherTile = kvp.Value;

            // other Tile not found in this Result, copy the other Tile
            if (!combinedTiles.TryGetValue(coord, out var currentTile)) {
                combinedTiles[coord] = otherTile;
                continue;
            }

            // both Results contain this Tile, accumulate the height for it
            var newChunk = CombineChunksAB(currentTile.Chunk, otherTile.Chunk);

            // construct a new Tile with the new Chunk that has the accumulated height
            var newTile = new Tile(currentTile.Coord.x, currentTile.Coord.y, currentTile.WorldPosition,newChunk);

            // inject the new Chunk into the Tile
            combinedTiles[coord] = newTile;
        }

        var combinedMeshes = new Dictionary<ITile, ImmutableMeshData>(TileMeshes);
        foreach (var meshPair in other.TileMeshes) {
            combinedMeshes[meshPair.Key] = meshPair.Value;
        }

        return new Result(combinedTiles, combinedMeshes);
    }

    public IResult GetTileResult(ITile tile) {
        throw new NotImplementedException();
    }

    static ITile.TileChunk CombineChunksAB(ITile.TileChunk a, ITile.TileChunk b) {
        var newChunk = new ITile.TileChunk(0);

        // get the Cells in Chunk A and B
        for (var i = 0; i < a.CellCount; i++) {
            var cellA = a[i];
            // find the matching Cells
            var matchingCell = FindMatchingCell(cellA, b);
            if (matchingCell != null) {
                // add the height from Cell B into Cell A
                var posA = cellA.WorldPosition;
                var posB = matchingCell.Value.WorldPosition;
                var combinedY = posA.y + posB.y;
                var newPos = new Vector3(posA.x, combinedY, posA.z);

                // create new Cell instance with combined position
                var pos = new float[3] { newPos.x, newPos.y, newPos.z };
                var id = cellA.ID;
                var color = new float[3] { cellA.Color.r, cellA.Color.g, cellA.Color.b };
                var combinedCell = new Cell(pos, id, color);
                newChunk.AddCell(combinedCell);
                newChunk.CellElevations.Add(combinedY);
            }
        }

        return newChunk;

        Cell? FindMatchingCell(Cell source, ITile.TileChunk otherChunk) {
            foreach (var otherCell in otherChunk.Cells) {
                if (source.ID == otherCell.ID) {
                    return otherCell;
                }
            }

            return null;
        }
    }

    /// <summary> creates a new Result with no data inside </summary>
    /// <returns> a new Result storing no data </returns>
    public static Result CreateEmptyInstance() {
        return new Result(new Dictionary<Vector2Int, ITile>(), new Dictionary<ITile, ImmutableMeshData>());
    }

    /// <summary>
    ///     create a new Result that contains this Tile
    /// </summary>
    /// <param name="tile"> the Tile to store immediately in the Result </param>
    /// <returns> a new Result that contains a single Tile </returns>
    public static Result CreateTileResult(ITile tile) {
        // store this Tile in the dictionary
        var tiles = new Dictionary<Vector2Int, ITile> { { tile.Coord, tile } };
        return new Result(tiles, new Dictionary<ITile, ImmutableMeshData>());
    }*/
}
}