using UnityEngine;

namespace VoroSystem.Grids {
public interface ITile {
    Vector2 Position { get; }
    bool Visible { get; }
    bool Dirty { get; }
    StateType StateType { get; }
    void Update();
}
}