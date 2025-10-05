using UnityEngine;

namespace VoroSystem.GridSystem {
/// <summary>
///     Point for a Chunk
/// </summary>
public class Cell : Point {
    public Cell(Vector3 position, int id, Color color) : base(position, id) {
        Color = color;
    }

    public Color Color { get; }
}
}