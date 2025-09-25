using UnityEngine;

namespace VoroWorld {
public struct ChunkPoint {
    public Vector3 Position;
    public int ID;

    public ChunkPoint(Vector3 position, int id) {
        Position = position;
        ID = id;
    }
}
}