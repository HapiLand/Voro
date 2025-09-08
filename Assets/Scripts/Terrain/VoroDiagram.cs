using System.Text;
using UnityEngine;

namespace Terrain {
public class VoroDiagram {
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
    // ToDo implement dictionary for geometry <GeoMap, Geometry[]> to allow for variants to be stored

    /// <summary>
    ///     the origin of the diagram, this is a leftover property
    ///     from when VoroDiagram was used as the replacement of the Voro class
    /// </summary>
    public Vector2 Origin;

    /// <summary>
    ///     the index of this array maps to a collection of Points
    /// </summary>
    public int[] PointMap;

    /// <summary>
    ///     the points within the diagram
    ///     vector3 - position
    ///     int - piece ID
    /// </summary>
    public (Vector3, int)[] Points;
    // ToDo stop using tuple, as the int value is what is stored by GeoMap

    // ToDo implement data properties for diagram like Color

    public override string ToString() {
        // build a string that will display the contents of this diagram

        var sb = new StringBuilder();

        sb.AppendLine("VoroDiagram:");
        sb.AppendLine("  PointMap:");
        if (PointMap != null) {
            for (var i = 0; i < PointMap.Length; i++) {
                sb.AppendLine($"    [{i}] -> {PointMap[i]}");
            }
        }
        else {
            sb.AppendLine("    (null)");
        }

        sb.AppendLine("  Points:");
        if (Points != null) {
            for (var i = 0; i < Points.Length; i++) {
                var (position, pieceId) = Points[i];
                sb.AppendLine($"    [{i}] -> Position: {position}, Piece ID: {pieceId}");
            }
        }
        else {
            sb.AppendLine("    (null)");
        }

        return sb.ToString();
    }


    // ToDo reimplement what is in the comment block as needed
    /*
    /// <summary>
    ///     originates from DemoTable.json
    ///     this is only set once when the VoroDiagram is constructed
    ///     it acts as the configuration that all voros for that point table
    ///     ---
    ///     a practical example is for biomes, two DemoTable.json will have
    ///     rules for what can be generated, a VoroDiagram created with both
    ///     will have two different forms of Configuration
    /// </summary>
    Configuration _configuration;

    /// <summary>
    ///     related to pointMap so an fbx model can be matched with
    ///     the correct point index
    /// </summary>
    int[] _geometryMap;

    /// <summary>
    ///     turns a _geometryMap array into a 3D mesh format
    ///     this is intended to be a friendlier way for Unity
    ///     to read the mesh data for instantiation
    /// </summary>
    GeometryBuilder _meshBuilder;

    /// <summary>
       ///     this is data from DemoTable
       ///     the exact center point of each voronoi cell
       ///     this is a vertex in a graph, which allows
       ///     for adjacency look up is theoretically possible
       /// </summary>
       Centroids[] _centroids;

    /// <summary>
       ///     allows for the contents of Voro to be marked
       ///     to produce a hard coded rules in generation
       ///     eg for player spawn, death barrier, ocean
       /// </summary>
       class Tags { }

           /// <summary>
       ///     this is data from DemoTable
       /// </summary>
       class Configuration {
           object[] _bar;
           object[] _foo;
           Color[] _pointColors;
           Vector3[] _positions;
       }

    public VoroDiagram(Configuration configuration) {
        // this configuration puts the voro into a default state
        // as if no height has been set, just an unset voro
        Configuration = configuration;
    }

    public Configuration Configuration {
        get => _configuration;
        set
        {
            if (_configuration != null) {
                Debug.LogError("configuration has already been set");
                return;
            }

            Debug.Log("new configuration");
            // unpack any values from the configuration
            // so that is fit for use
            _configuration = value;

            Debug.Log("new points array");
            // read configuration.points to produce this
            // each integer maps to each point
            _pointMap = new int[0];

            Debug.Log("new geometry array");
            // read configuration.points and configuration.geometries
            // make it so each integer maps a pointmap to the geometries
            _geometryMap = new int[0];
        }
    }
    */
}
}