using UnityEngine;

namespace VoroSystem.World.Generate {
public struct MeshVertex {
    public Vector2 Position;
    public float Height;

    public MeshVertex(Vector3 pos) {
        Position = new Vector2(pos.x, pos.z);
        Height = pos.y;
    }

    public Vector3 WorldPosition => new(Position.x, Height, Position.y);
}
}