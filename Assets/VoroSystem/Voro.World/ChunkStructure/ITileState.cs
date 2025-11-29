using UnityEngine;

namespace VoroSystem.Voro.World.ChunkStructure {
/// <summary>
/// implementation for the state of a Tile
/// </summary>
public interface ITileState {
    bool Initialised { get; }
    bool Visible { get; }
    void Update();
    void Init(Transform parent);
}
}