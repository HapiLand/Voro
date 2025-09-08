using System.Text;
using UnityEngine;

namespace ConfigEditor.V2 {
public class Diagram {
    /// <summary>
    ///     the index of this array maps to a collection of fbx geo
    ///     these are the piece ID values each point uses
    /// </summary>
    public int[] GeoMap;

    /// <summary>
    ///     the points within the diagram
    ///     vector3 - position
    ///     int - piece ID
    /// </summary>
    public GameObject[] Geometry;

    /// <summary>
    ///     the origin of the world tile the diagram is part of
    /// </summary>
    public (int x, int z) Origin;

    // ToDo implement dictionary for geometry <GeoMap, Geometry[]> to allow for variants to be stored
    /// <summary>
    ///     the index of this array maps to a collection of Points
    /// </summary>
    public int[] PointMap;

    /// <summary>
    ///     the points within the diagram
    ///     vector3 - position
    /// </summary>
    public Vector3[] Points;

    // ToDo implement data properties for diagram like Color
    public Diagram((int x, int z) origin) {
        Origin = origin;
    }

    public override string ToString() {
        // build a string that will display the contents of this diagram

        var sb = new StringBuilder();
        sb.AppendLine($"VoroDiagram: [{Origin.x},{Origin.z}]");
        sb.AppendLine("  PointMap:");
        sb.AppendLine(PointMap != null ? $"    has {PointMap.Length} points" : "    (null)");
        return sb.ToString();
    }
}
}