using UnityEngine;

namespace Voro.World.Internal {
struct Coordinate {
    public readonly int X;
    public readonly int Z;

    public Coordinate(int x, int z) {
        X = x;
        Z = z;
    }

    public Vector3 WorldPosition() {
        return new Vector3(X, 0f, Z);
    }
}
}