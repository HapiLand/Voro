using UnityEngine;

namespace VoroWorld.Grids {
/// <summary>
///     a tile object for the game world
/// </summary>
public class Tile {
    /// <summary>
    ///     the Container object is what will contain the cell object instances
    /// </summary>
    public GameObject Container;

    public Vector3 Position;

    public Tile(Vector3 pos) {
        Position = pos;
        // create the container object as this is what will contain the cell objects
        Container = new GameObject($"Tile [{pos.x:F0},{pos.z:F0}]");
        Container.transform.position = Position;
    }
}
}