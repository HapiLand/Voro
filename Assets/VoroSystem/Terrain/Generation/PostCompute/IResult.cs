using System.Collections.Generic;
using UnityEngine;
using VoroSystem.WorldGrid.Grids;

namespace VoroSystem.Terrain.Generation.PostCompute {
/// <summary> represents the complete output of IGenerator, the Terrain Generation process </summary>
public interface IResult {
    /*/// <summary> all generated Tiles and their Computed state </summary>
    IReadOnlyDictionary<Vector2Int, ITile> Tiles { get; }

    /// <summary> Terrain Mesh generated each Tile </summary>
    IReadOnlyDictionary<ITile, ImmutableMeshData> TileMeshes { get; }

    /// <summary> merge another Result with this one, produce a composite Result </summary>
    IResult Combine(IResult other);

    /// <summary> retrieve the Result data for a single Tile </summary>
    IResult GetTileResult(ITile tile);*/
}
}