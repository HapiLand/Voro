using System;
using Newtonsoft.Json.Linq;
using UnityEngine;

namespace Voro.Generation.Internal {
class PointArray {
    public PointArray(TextAsset asset) {
        var parsedPoints = JObject.Parse(asset.text)["Points"]?.ToObject<ParsedPoint[]>();
        if (parsedPoints == null) {
            Points = Array.Empty<WorldPoint>();
            return;
        }

        Points = new WorldPoint[parsedPoints.Length];
        for (var i = 0; i < parsedPoints.Length; i++) {
            WorldPoint.CreateInstance(parsedPoints[i], out var chunkPoint);
            Points[i] = chunkPoint;
        }
    }

    public WorldPoint[] Points { get; }
}
}