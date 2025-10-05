using UnityEngine;

namespace VoroSystem.Interface {
public interface IPoint {
    Vector2 XY { get; }
    float Height { get; set; }
    Vector3 Position { get; }
    int ID { get; }
}
}