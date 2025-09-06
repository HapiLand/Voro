using UnityEngine;

namespace Terrain {
//  TBA Voro
//  |    Point[]
//  |    Mesh[]
//  |
//  |    Configuration
//  |    |    <pos>
//  |    |    <id>
//  |    |    <foo>
//  |    |    <bar>
public class Voro {
    /// <summary>
    ///     originates from DemoTable.json
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
    ///     array shall be provided to GPU to be processed
    ///     this will find the elevation of the terrain
    /// </summary>
    int[] _pointMap;

    public Voro(Configuration configuration) {
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
}
}