using UnityEngine;

namespace Terrain {
/// <summary>
///     end user >
///     editor >
///     json >
///     diagram >
///     voro >
///     voroHeight >
///     geometry builder >
///     unity
/// </summary>
public class Diagram {
    /// <summary>
    ///     this is data from DemoTable
    ///     the exact center point of each voronoi cell
    ///     this is a vertex in a graph, which allows
    ///     for adjacency look up is theoretically possible
    /// </summary>
    Centroids[] _centroids;

    /// <summary>
    ///     this is data from DemoTable
    ///     the map of every point as to be provided to
    ///     gpu buffer so the correct point can be selected
    /// </summary>
    Point[] _pointMap;

    /// <summary>
    ///     this is data from DemoTable
    ///     there is a global resource of fbx to pick from
    ///     all need be stored is a tuple to select the
    ///     correct instance later
    /// </summary>
    (int piece, int variant)[] _prefabMap;

    /// <summary>
    ///     this is data from DemoTable
    /// </summary>
    class Configuration {
        object[] _bar;
        object[] _foo;
        Color[] _pointColors;
        Vector3[] _positions;
    }

    /// <summary>
    ///     allows for the contents of Voro to be marked
    ///     to produce a hard coded rules in generation
    ///     eg for player spawn, death barrier, ocean
    /// </summary>
    class Tags { }
}
}