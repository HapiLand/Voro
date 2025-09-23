using UnityEngine;
using VoroTileMap;

namespace VoroGeneration {
/// <summary>
///     an object used to generate terrain
/// </summary>
public class Diagram {
    /// <summary>
    ///     holds the game world map of tiles
    /// </summary>
    readonly TileMap _tileMap;

    /// <summary>
    ///     so objects can be added to the scene
    /// </summary>
    readonly WorldMapController _worldMapController;

    public Diagram(WorldMapController worldMapController, TileMap tileMap) {
        Debug.Log("Creating Diagram");
        _worldMapController = worldMapController;
        _tileMap = tileMap;
        
        
        
        /*
         * the Diagram has all the tiles, now generate the Chunks
         * Chunk =  the parsed PointTable.json data that exists for each Tile
         */

        /*
         * 1) create tiles
         * 2) then make diagram
         * ^ DONE ^
         *
         * 3) diagram creates point table
         * 4) run initial compute
         *      this uses default effect with no provided data, so height set to 0
         *      compute returns its result to the diagram
         *      use result to instantiate mesh assets
         *
         * the outcome of this shows a flat 0 height scene with GameObjects for each FBX
         *
         * repeat this later when the Diagram can access the UI Editors data
         */
    }


    /*
    /// <summary>
    ///     the output from the shader
    /// </summary>
    readonly VoroResult _computeReturn;

    /// <summary>
    ///     user created layers.
    ///     for each generation function array
    /// </summary>
    readonly LayerInfo _computeThreadData;

    /// <summary>
    ///     user set values to drive generation
    /// </summary>
    readonly IControlData _controlElementData;

    /// <summary>
    ///     the seed object that produces the effect
    /// </summary>
    readonly INode _generatorDataRequirements;

    /// <summary>
    ///     produces a result via its function
    /// </summary>
    readonly IEffect _generatorFunction;

    /// <summary>
    ///     handle diagram events
    /// </summary>
    readonly DiagramManager _manager;

    /// <summary>
    ///     a game object entity that represents the tile
    /// </summary>
    readonly Tile _tile;
    */
}
}