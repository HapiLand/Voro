using UnityEngine;

namespace VoroWorld {
[ExecuteAlways]
public class World : MonoBehaviour {
    /// <summary>
    ///     manages the tile diagrams for the world
    /// </summary>
    TileMap _tileMap;

    /// <summary>
    ///     this is the parent object to store the GameObjects for every Tile
    /// </summary>
    Transform _tileContainer;

    /// <summary>
    ///     to run the generation system
    /// </summary>
    VoroCompute _voroCompute;

    void Awake() {
        _tileContainer = new GameObject("Tile Container").transform;
        _tileContainer.SetParent(gameObject.transform);

        _tileMap = new TileMap();
        // called once the VoroDiagram map has been fully created
        // DiagramManager.OnMapConstructed += OnConstructedAllDiagrams;
        _tileMap.DiagramMapConstructed += () => {
            Debug.Log("All VoroDiagrams have been constructed");
            // instantiate the Mesh GameObjects within the all the diagrams
            _tileMap.InstanceDiagramObjects(_tileContainer);
        };

        _voroCompute = new VoroCompute(_tileMap);

        // notify listeners that the WorldManager is ready
        _tileMap.CreateDiagramMap();
    }
}
}