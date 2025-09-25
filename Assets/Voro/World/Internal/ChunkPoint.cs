using UnityEngine;

namespace Voro.World.Internal {
public struct ChunkPoint {
    public static void CreateInstance(ParsedPoint parsedPoint, out ChunkPoint chunkPoint) {
        chunkPoint = new ChunkPoint(
            new Vector3(parsedPoint.Pos[0], 0, parsedPoint.Pos[1]),
            parsedPoint.Id,
            new Color(parsedPoint.Col[0], parsedPoint.Col[1], parsedPoint.Col[2], 1.0f));
    }

    public Vector3 Position;
    public int ID;
    public Color Color;

    ChunkPoint(Vector3 position, int id, Color color) {
        Position = position;
        ID = id;
        Color = color;
    }
}
}