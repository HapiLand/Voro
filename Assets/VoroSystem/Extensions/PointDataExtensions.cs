using UnityEngine;
using VoroSystem.GridSystem;

namespace VoroSystem.Extensions {
public static class PointDataExtensions {
    public static Cell ToCell(this PointData pointData) {
        return new Cell(pointData.p, pointData.id, new Color(pointData.col.x, pointData.col.y, pointData.col.z, 1.0f));
    }
}
}