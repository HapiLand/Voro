using UnityEngine;

namespace VoroSystem.Voro.World.Map {
public interface ITile {
    int Index { get; }
    Vector2 Position { get; }
    float Size { get; }
    bool Visible { get; }
}
}