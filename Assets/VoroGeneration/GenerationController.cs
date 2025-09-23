using JetBrains.Annotations;
using UnityEngine;
using VoroTileMap;

namespace VoroGeneration {
/// <summary>
///     class that handles the generation system
///     ---
///     takes all the editor and world data, turns that into a Diagram
///     ---
///     executes VoroCompute to process that Diagram
///     ---
///     writes the result into the world scene
/// </summary>
public class GenerationController {
    [CanBeNull] static GenerationController _instance;
    static readonly object _lock = new();

    GenerationController(WorldMapController worldMapController, TileMap tileMap) {
        Diagram = new Diagram(worldMapController, tileMap);
    }

    public Diagram Diagram { get; }

    public static GenerationController GetInstance(WorldMapController worldMapController, TileMap tileMap) {
        if (_instance == null) {
            lock (_lock) {
                if (_instance == null) {
                    Debug.Log("Creating instance of GenerationController");
                    _instance = new GenerationController(worldMapController, tileMap);
                }
            }
        }

        return _instance;
    }
}
}