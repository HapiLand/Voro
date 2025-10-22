using UnityEngine;

namespace Voro.Core.Map {
class Tile : ITile {
    public Tile(Vector2 position) {
        Position = position;
    }

    public Vector2 Position { get; set; }
}
}