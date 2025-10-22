using System.Text;
using UnityEngine;

namespace Voro.Core.Bounds {
/// <summary> Serves as space where the World exists </summary>
class Landscape {
    readonly Region _bounds;
    readonly GridInfo _grid;

    public Landscape(LandscapeBuilder builder) {
        _grid = new GridInfo(builder.GridScale);
        _bounds = new Region((builder.XBoundSize, builder.ZBoundSize), builder.Position, builder.Margin);
    }

    (float x, float z) BoundingSize => _bounds.BoundingSize;
    public (int x, int y) GridSize => _grid.GridSize(_bounds.BoundingSize);

    public string GetDescription() {
        var sb = new StringBuilder();
        sb.Append($"Landscape is {BoundingSize.x},{BoundingSize.z}. ");
        sb.Append($"It has a {GridSize.x}x{GridSize.y} Grid. ");
        return sb.ToString();
    }

    /// <summary> Information that determines how the World Map appears within the Landscape </summary>
    readonly struct GridInfo {
        /// <summary> The size of each tile within the Grid </summary>
        readonly float _gridScale;

        public GridInfo(float gridScale) {
            _gridScale = gridScale;
        }

        public (int x, int y) GridSize((float x, float z) boundingSize) {
            var x = _gridScale * boundingSize.x;
            var y = _gridScale * boundingSize.z;
            return ((int)x, (int)y);
        }
    }

    /// <summary> The bounding rectangle that sets the region where the Landscape exists inside </summary>
    readonly struct Region {
        /// <summary> The size of the Regions rectangle </summary>
        readonly (float x, float z) _dimensions;

        /// <summary> The origin position of the Landscape </summary>
        readonly Vector2 _position;

        /// <summary> The outer space of the region, outside the border </summary>
        readonly float _margin;

        public Region((float x, float z) dimensions, Vector2 position, float margin) {
            _dimensions = dimensions;
            _position = position;
            _margin = margin;
        }

        public (float x, float z) BoundingSize => (_dimensions.x + _margin, _dimensions.z + _margin);
    }
}
}