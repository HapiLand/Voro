using UnityEngine;

namespace Voro.Systems.Landscape {
/// <summary>
/// Interactive object acting as a corner of a <see cref="BoundaryGizmo" />
/// </summary>
class Handle {
    Vector2 _position;

    public Handle(Vector2 position) {
        _position = position;
    }

    public Vector2 Position {
        get => _position;
        set
        {
            if (_position == value) {
                return;
            }

            _position = value;
            // PositionChanged?.Invoke(this);
        }
    }

    // public event Action<Handle> PositionChanged;
}
}