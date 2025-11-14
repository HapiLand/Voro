using UnityEngine;

namespace VoroSystem.Generation.MesherSystem {
public struct MeshVertex {
    public Vector2 position;
    public float height;

    public MeshVertex(Vector3 pos) {
        position = new Vector2(pos.x, pos.z);
        height = pos.y;
    }

    public Vector3 WorldPosition => new(position.x, height, position.y);
}
}