using System.Collections.Generic;
using UnityEngine;

namespace VoroSystem.Voro.World.ChunkStructure {
/// <summary>
/// implementation for the mesh of a Tile
/// </summary>
public interface ITileMesh {
    MeshFilter Filter { get; }
    Mesh Mesh { get; }
    CVertex[] Vertices { get; }
    List<int> Triangles { get; }
    Vector2[] UVs { get; }
}
}