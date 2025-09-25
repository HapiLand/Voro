using UnityEngine;

namespace Voro.Grids {
public static class CoordinateExtensions {
    public static Vector3 WorldPosition(this Coordinate coord) {
        return new Vector3(coord.X, 0f, coord.Z);
    }
}
}