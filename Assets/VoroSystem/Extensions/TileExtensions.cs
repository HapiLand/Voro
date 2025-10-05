using UnityEngine;

namespace VoroSystem.Extensions {
public static class TileExtensions
{
    public static Vector3 WorldPosition(this Tile tile)
    {
        var coord = new Vector2Int(0, 0);
        return new Vector3(coord.x, 0f, coord.y);
    }
}
}