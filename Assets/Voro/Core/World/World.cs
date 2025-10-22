using UnityEngine;
using Voro.Core.Bounds;
using Voro.Core.Map;

namespace Voro.Core.World {
/// <summary> A bounding region with a 2D grid </summary>
public class World {
    public TileMap Map;

    /// <summary> Constructor for the World </summary>
    public World() {
        // Create the region where this World exists
        var landscape = new LandscapeBuilder(1)
            .SetMargin(0)
            .SetPosition(Vector2.zero)
            .SetBoundSize(10, 10)
            .Build();
        Debug.Log($"[World] {landscape.GetDescription()}");

        // Create the space to later populate with pieces of Terrain
        Debug.Log("[World] Generate Map");
        var builder = new TileMapBuilder(landscape);
        builder.Build();
        Map = builder.TileMap;
    }
}
}