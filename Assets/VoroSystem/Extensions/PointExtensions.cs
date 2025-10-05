using UnityEngine;
using VoroSystem.Interface;

namespace VoroSystem.Extensions {
public static class PointExtensions
{
    public static Vector2 ToXY(this Vector3 vector)
    {
        return new Vector2(vector.x, vector.z);
    }
    
    public static Vector3 WorldPosition(this IPoint point)
    {
        return new Vector3(point.XY.x, point.Height, point.XY.y);
    }
}
}