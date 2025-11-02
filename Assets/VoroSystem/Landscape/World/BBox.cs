using UnityEngine;

namespace VoroSystem.Landscape.World {
/// <summary>
/// An axis aligned bounding box.
/// </summary>
public struct BBox {
    /// <summary>
    /// The lower vertex
    /// </summary>
    public Vector2 TopLeft;

    /// <summary>
    /// The upper vertex
    /// </summary>
    public Vector2 BottomRight;

    public BBox(Vector2 min, Vector2 max)
        : this(ref min, ref max) { }

    public BBox(ref Vector2 min, ref Vector2 max) {
        TopLeft = min;
        BottomRight = max;
    }

    public BBox(Vector2 center, float width, float height) {
        TopLeft = center - new Vector2(width / 2, height / 2);
        BottomRight = center + new Vector2(width / 2, height / 2);
    }

    public Vector2 Size => BottomRight - TopLeft;


    /// <summary>
    /// Get the center of the AABB.
    /// </summary>
    public Vector2 Center => 0.5f * (TopLeft + BottomRight);

    /// <summary>
    /// Get the extents of the AABB (half-widths).
    /// </summary>
    public Vector2 Extents => 0.5f * (BottomRight - TopLeft);


    /// <summary>
    /// Verify that the bounds are sorted.
    /// </summary>
    /// <returns>
    /// <c>true</c> if this instance is valid; otherwise, <c>false</c>.
    /// </returns>
    public bool IsValid() {
        var d = BottomRight - TopLeft;
        return d.x >= 0.0f && d.y >= 0.0f;
    }
}
}