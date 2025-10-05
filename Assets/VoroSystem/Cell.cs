using UnityEngine;
using VoroSystem.Extensions;
using VoroSystem.Interface;

namespace VoroSystem {
public class Cell : IPoint, IPointData {
    public Cell(Vector3 position, int id, Color color) {
        XY = position.ToXY();
        ID = id;
        Color = color;
    }

    public Vector2 XY { get; }
    public float Height { get; set; }
    public Vector3 Position => new(XY.x, Height, XY.y);
    public int ID { get; }
    public Color Color { get; }
}
}