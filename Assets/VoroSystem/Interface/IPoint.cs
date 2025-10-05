using UnityEngine;

namespace VoroSystem.Interface {
public interface IPoint {
    Vector3 Position { get; }
    int ID { get; }
}
}