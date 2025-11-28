using UnityEngine;
using VoroSystem.Voro.World.TileEntities;

namespace VoroSystem.Voro.World.TileStructure {
/// <summary>
/// implementation for the mesh of a Tile
/// </summary>
public interface ITileMesh {
  Mesh Mesh { get; }
  Vertex[] Vertices { get; }
  MeshFilter Filter { get; }
}
}