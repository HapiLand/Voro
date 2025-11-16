using System;
using UnityEngine;

namespace VoroSystem.Generation.TerrainSystem {
[Serializable]
public struct Vertex {
    public Vector3 position;

    public Vertex(Vector3 pos) {
        position = pos;
    }
}
}