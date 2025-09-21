using System.Collections.Generic;
using UnityEngine;
using VoroUI.EditorTabs;
using VoroWorld.Diagrams;
using VoroWorld.Generation;
using VoroWorld.Generation.Effects.Base;

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
    ///     generates world terrain
    /// </summary>
    VoroCompute _voroCompute;

    void Awake() {
        _tileContainer = new GameObject("Tile Container").transform;
        _tileContainer.SetParent(gameObject.transform);

        _diagramManager = new DiagramManager();
        // called once the VoroDiagram map has been fully created
        _diagramManager.DiagramMapConstructed += () => {
            Debug.Log("All VoroDiagrams have been constructed");
            // instantiate the Mesh GameObjects within the all the diagrams
            _diagramManager.InstanceDiagramObjects(_tileContainer);
        };

        // create VoroCompute so the WorldManager is capable of executing the terrain generation
        _voroCompute = new VoroCompute();
        LayersNodesController.OnRecompute += OnWorldUpdate;

        // notify listeners that the WorldManager is ready
        _diagramManager.CreateDiagramMap();
    }

    void OnDestroy() {
        // unsubscribe to the static event in the UI
        LayersNodesController.OnRecompute -= OnWorldUpdate;
    }

    void OnWorldUpdate(Dictionary<string, List<IEffect>> effectDict) {
        // get the VoroDiagram data so it can be provided to VoroCompute

        // initiate terrain generation
        _voroCompute.ComputeWorldTerrain(effectDict, _diagramManager.GetDiagramMapArray(), out var result);
        Debug.Log($"VoroCompute completed its execute and returned {result.Length} results");

        // the results will be applied to the diagrams to show the newly generated result
        _diagramManager.AppendResultsToMap(result);
    }
}
}