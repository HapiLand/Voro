using System.Collections.Generic;
using UnityEngine;

namespace VoroSystem.Voro.World.ChunkStructure.Interfaces {
/// <summary>
/// implementation for the mesh of a Tile
/// </summary>
public interface IChunkMesh {
  MeshFilter Filter { get; }
  Mesh Mesh { get; }
  MeshVertex[] Vertices { get; }
  List<int> Triangles { get; }
  Vector2[] UVs { get; }
  void BuildMesh();
  void UpdateHeight();
}
}