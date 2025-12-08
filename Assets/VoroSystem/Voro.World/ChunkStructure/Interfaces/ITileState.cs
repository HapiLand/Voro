using UnityEngine;

namespace VoroSystem.Voro.World.ChunkStructure.Interfaces {
/// <summary>
/// implementation for the state of a Tile
/// </summary>
public interface ITileState {
  bool Initialised { get; set; }
  bool Visible { get; }
  void UpdateVisibility(Vector2 position);
}
}