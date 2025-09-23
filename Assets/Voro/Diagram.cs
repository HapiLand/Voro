using UnityEngine;

namespace Voro {
/// <summary>
///     - Core structure storing terrain layout.
///     - Receives cell point array from Chunk.
///     - Receives dictionary from Editor.
///     - Outputs positional+mesh data for all Tiles.
/// </summary>
public class Diagram {
    public readonly Chunk _chunk;
    readonly TileMap _tileMap;
    readonly VoroUI _userInterface;

    /// <summary>
    /// </summary>
    /// <param name="tileMap">world map position array</param>
    /// <param name="voroUI">generation instructions</param>
    public Diagram(TileMap tileMap, VoroUI voroUI) {
        // TileMap
        _tileMap = tileMap;
        _tileMap.GenerateMap();

        // Chunk
        _chunk = new Chunk();

        // User Interface
        _userInterface = voroUI;
    }

    public void Dispose() { }

    void OnMapGenerated() {
        Debug.Log("TileMap Generated");
    }
}
}