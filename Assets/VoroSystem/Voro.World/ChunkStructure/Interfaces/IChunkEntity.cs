using UnityEngine;

namespace VoroSystem.Voro.World.ChunkStructure.Interfaces {
/// <summary>
/// implementation for the GameObject representation of a Tile
/// </summary>
public interface IChunkEntity {
  Vector3 Position { get; }
  GameObject Entity { get; }
  ChunkMaterial ChunkMaterial { get; }
  ChunkMesh ChunkMesh { get; }
}
}