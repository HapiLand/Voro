using Newtonsoft.Json.Linq;
using UnityEngine;

namespace Voro.World.Internal {
public class ChunkPointArray {
    public ChunkPoint[] Points;

    public ChunkPointArray(TextAsset asset) {
        Parse<ParsedPoint>(asset, "Points", out var parsedPoints);
        ToChunkPoints(parsedPoints);
    }

    void ToChunkPoints(ParsedPoint[] table) {
        Points = new ChunkPoint[table.Length];
        for (var i = 0; i < table.Length; i++) {
            ChunkPoint.CreateInstance(table[i], out var chunkPoint);
            Points[i] = chunkPoint;
        }
    }

    static void Parse<T>(TextAsset asset, string arrayName, out T[] table) {
        table = JObject.Parse(asset.text)[arrayName]?.ToObject<T[]>();
    }
}
}