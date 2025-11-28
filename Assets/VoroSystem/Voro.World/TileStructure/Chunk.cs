using UnityEngine;

namespace VoroSystem.Voro.World.TileStructure {
/// <summary>
/// represents an object that exists within a map
/// represents an object that exists within a scene
/// </summary>
public class Chunk : IMapTile {
  public int Index { get; }
  public float Size { get; }
}
}