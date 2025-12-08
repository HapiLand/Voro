using UnityEngine;
using VoroSystem.Voro.World.Components;

namespace VoroSystem.Voro.World.ChunkStructure.Interfaces {
/// <summary>
/// implementation for the GameObject representation of a Tile
/// </summary>
public interface ITileEntity {
  Vector2 Position { get; }
  GameObject Instance { get; }
  TileMaterial TileMaterial { get; set; }
  TileMesh TileMesh { get; set; }
  void CreateInstance(Transform parent, float size, VoroMap map);
  void UpdateHeight();
}
}