using UnityEngine;

namespace VoroSystem.Voro.World.ChunkStructure.Interfaces {
/// <summary>
/// implementation for the heightmap of a Tile
/// </summary>
public interface ITileMaterial {
  MeshRenderer Renderer { get; }
  void SetMaterial(Material mat);
  void SetTexture(Texture2D tex);
  Material GetMaterial();
}
}