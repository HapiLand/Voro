using UnityEngine;
using VoroSystem.Interface;

namespace VoroSystem {
public abstract class Point : IPoint {
    protected Point(Vector3 position, int id) {
        Position = position;
        ID = id;
    }

    public Vector3 Position { get; protected set; }
    public int ID { get; }
}
}