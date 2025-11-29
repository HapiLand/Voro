using UnityEngine;

namespace VoroSystem.Voro.World.ChunkStructure {
/// <summary>
/// implementation for the GameObject representation of a Tile
/// </summary>
public interface ITileEntity {
    Vector2 Position { get; }
    GameObject Instance { get; }
}
}