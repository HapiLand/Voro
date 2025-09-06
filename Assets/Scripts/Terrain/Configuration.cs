using UnityEngine;

namespace Terrain {
/// <summary>
/// the configuration that originates in DemoTable.json
/// is used to define the default state of Voro
///
/// shall also impose rules for the terrain generation
/// different Tables may be used for different Biomes
/// </summary>
public class Configuration {
    Color[] _colors;

    /// <summary>
    ///     the many FBX files
    /// </summary>
    Geometry[] _geometries;

    (Vector3 pos, int id)[] _points;
}
}