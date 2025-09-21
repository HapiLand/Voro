using System;
using UnityEngine;
using VoroWorld.Diagrams;

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

    void Awake() {
        _tileContainer = new GameObject("Tile Container").transform;
        _tileContainer.SetParent(gameObject.transform);

        _diagramManager = new DiagramManager();
        // called once the VoroDiagram map has been fully created
        DiagramManager.OnMapConstructed += OnConstructedAllDiagrams;

        // notify listeners that the WorldManager is ready
        OnWorldManagerAwake?.Invoke();
    }

    void OnDestroy() {
        OnWorldManagerAwake = null;
    }

    void OnConstructedAllDiagrams() {
        // Debug.Log("VoroDiagram Map Constructed");

        // create the tiles for the world scene
        OnCreatedAllDiagrams?.Invoke(_tileContainer);
    }

    public static event Action<Transform> OnCreatedAllDiagrams;
    public static event Action OnWorldManagerAwake;
}
}