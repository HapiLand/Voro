using EditorGUI.Source.Effects.Base;
using EditorGUI.Source.Voro.Grids;
using UnityEngine;

namespace EditorGUI.Source.Voro {
/// <summary>
///     manages the Unity GameWorld Scene, computes values from the Editor to generate the actual game scene
/// </summary>
[ExecuteAlways]
public class GameWorldManager : MonoBehaviour {
    [SerializeField] int width = 1;
    [SerializeField] int height = 1;

    TileGrid _tileGrid;
    public static GameWorldManager Instance { get; private set; }

    void Awake() {
        if (Instance != null && Instance != this) {
            DestroyImmediate(this);
        }

        var numChildren = transform.childCount;
        for (var i = numChildren - 1; i >= 0; i--) {
            DestroyImmediate(transform.GetChild(i).gameObject);
        }

        Instance = this;

        EffectBase.OnEffectChanged += OnEffectChanged;
        Debug.Log("GameWorldManager.Awake - called after the subscription");
        InitializeWorldGrid();
    }

    void OnEffectChanged(IEffect effect) {
        Debug.Log($"Effect.{effect.Name} has changed");
        ExecuteComputeWorld();
    }

    void InitializeWorldGrid() {
        // Debug.Log("initailise world grid");
        _tileGrid = new TileGrid(width, height, transform);
    }

    public Grids.WorldTile GetTile(int x, int z) {
        return _tileGrid?.GetTile(x, z);
    }

    public (int width, int height) GetGridDimensions() {
        return _tileGrid?.Dimensions ?? (0, 0);
    }

    public void ExecuteComputeWorld() {
        Debug.Log("ExecuteComputeWorld");
        //     // Debug.Log($"compute: Diagram.{diagram.DisplayName}");
        //     // Debug.Log($"Diagram contains [{diagram.NodeInstances}] Effect(s)");
        //     for (var x = 0; x < _dimensions[0]; x++) {
        //         for (var z = 0; z < _dimensions[1]; z++) {
        //             Debug.Log("Execute Compute World");
        //             Debug.Log($"Tile ({x},{z})");
        //             // EditorCompute.Instance.DoCompute(ref _tiles[x, z]);
        //         }
        //     }
    }
}
}