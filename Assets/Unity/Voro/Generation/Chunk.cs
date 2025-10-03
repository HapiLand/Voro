using UnityEngine;
using Voro.Generation.Internal;

namespace Voro.Generation {
public class Chunk {
    public Chunk() {
        var asset = Resources.Load<TextAsset>("Table0");
        PointArray = new PointArray(asset);
    }

    PointArray PointArray { get; }
    public WorldPoint[] Points => PointArray.Points;
}
}