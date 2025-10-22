using UnityEngine;

namespace Voro.Core.Bounds {
class LandscapeBuilder {
    public readonly float GridScale;
    public float Margin;
    public Vector2 Position = Vector2.zero;
    public float XBoundSize = 1;
    public float ZBoundSize = 1;

    public LandscapeBuilder(float gridScale) {
        GridScale = gridScale;
    }

    /// <summary> The outer space of the region, outside the border </summary>
    public LandscapeBuilder SetMargin(float margin) {
        Margin = margin;
        return this;
    }

    /// <summary> The origin position of the Landscape </summary>
    public LandscapeBuilder SetPosition(Vector2 position) {
        Position = position;
        return this;
    }

    /// <summary> The X length of the bounds </summary>
    public LandscapeBuilder SetBoundSize(float x, float z) {
        XBoundSize = x;
        ZBoundSize = z;
        return this;
    }

    public Landscape Build() {
        return new Landscape(this);
    }
}
}