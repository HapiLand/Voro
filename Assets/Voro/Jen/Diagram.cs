using UnityEngine;
using Voro.Grids;
using Voro.UI;
using Voro.World;

namespace Voro.Jen {
/// <summary>
///     - Core structure storing terrain layout.
///     - Receives cell point array from Chunk.
///     - Receives dictionary from Editor.
///     - Outputs positional+mesh data for all Tiles.
/// </summary>
public class Diagram {
    readonly VoroUI _userInterface;
    public readonly Chunk Chunk;
    public readonly TileMap Map;

    /// <summary>
    /// </summary>
    /// <param name="map">world map position array</param>
    /// <param name="voroUI">generation instructions</param>
    public Diagram(TileMap map, VoroUI voroUI) {
        // TileMap
        Map = map;
        Map.GenerateMap();

        // Chunk
        Chunk = new Chunk();

        // User Interface
        _userInterface = voroUI;
    }


    public void Dispose() { }

    void OnMapGenerated() {
        Debug.Log("TileMap Generated");
    }
}
}