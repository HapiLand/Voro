using UnityEngine;

namespace VoroSystem.Voro.Utilities.Extensions {
public static class VectorExtensions {
    public static Vector2 ToVector2(this Vector3 vec) {
        return new Vector2(vec.x, vec.z);
    }

    public static Vector3 ToVector3(this Vector2 vec) {
        return new Vector3(vec.x, 0f, vec.y);
    }
}
}