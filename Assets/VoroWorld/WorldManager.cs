using UnityEngine;

namespace VoroWorld {
[ExecuteAlways]
public class WorldManager : MonoBehaviour {
    /// <summary>
    ///     manages the tile diagrams for the world
    /// </summary>
    DiagramManager _diagramManager;

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

        _diagramManager = new DiagramManager();
        // called once the VoroDiagram map has been fully created
        // DiagramManager.OnMapConstructed += OnConstructedAllDiagrams;
        _diagramManager.DiagramMapConstructed += () => {
            Debug.Log("All VoroDiagrams have been constructed");
            // instantiate the Mesh GameObjects within the all the diagrams
            _diagramManager.InstanceDiagramObjects(_tileContainer);
        };

        _voroCompute = new VoroCompute(_diagramManager);

        // notify listeners that the WorldManager is ready
        _diagramManager.CreateDiagramMap();
    }
}
}