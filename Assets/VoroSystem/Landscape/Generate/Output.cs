using System;
using System.Collections.Generic;
using UnityEngine;

namespace VoroSystem.Landscape.Generate {
/// <summary>
/// Class representing a Landscape
/// </summary>
/// <typeparam name="TSmartObject"></typeparam>
/// <typeparam name="TMeshChunk"></typeparam>
/// <typeparam name="TVoroPiece"></typeparam>
public class Output<TSmartObject, TMeshChunk, TVoroPiece> {
    // periodically in patches, the hill flattens out in a local region
    List<TSmartObject> _flatRegions;

    // a rectangular grid 4km x 1km
    // a hill of roughly 48° incline with a single path
    List<TMeshChunk> _map;

    // within the region high-end, the wall is a scattering of voronoi mesh pieces
    List<TVoroPiece> _voronoiPieces;

    public Output(List<TSmartObject> flatRegions, List<TMeshChunk> map, List<TVoroPiece> voronoiPieces) {
        _flatRegions = flatRegions;
        _map = map;
        _voronoiPieces = voronoiPieces;
    }

    public void Instantiate() {
        Debug.Log("Instantiating Landscape...");
        throw new NotImplementedException();
    }
}
}