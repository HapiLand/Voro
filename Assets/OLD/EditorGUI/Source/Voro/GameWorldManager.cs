using EditorGUI.Source.Effects.Base;
using EditorGUI.Source.Voro.Grids;
using UnityEngine;

namespace EditorGUI.Source.Voro {
/// <summary>
///     manages the Unity GameWorld Scene, computes values from the Editor to generate the actual game scene
/// </summary>
[ExecuteAlways]
public class GameWorldManager : MonoBehaviour {
    TileGrid _tileGrid;
    int width => 5;
    int height => 1;
    public static GameWorldManager Instance { get; private set; }

    void Awake() {
        if (Instance != null && Instance != this) {
            DestroyImmediate(this);
            return;
        }

        // clear any pre-existing children
        var numChildren = transform.childCount;
        for (var i = numChildren - 1; i >= 0; i--) {
            DestroyImmediate(transform.GetChild(i).gameObject);
        }

        Instance = this;

        EffectBase.OnEffectChanged += OnEffectChanged;
        InitializeWorldGrid();
    }

    void OnEffectChanged(IEffect effect) {
        Debug.Log($"Effect.{effect.Name} has changed");
        ExecuteComputeWorld();
    }

    void InitializeWorldGrid() {
        _tileGrid = new TileGrid(width, height, transform);
    }

    public WorldTile GetTile(int x, int z) {
        return _tileGrid?.GetTile(x, z);
    }

    public void ExecuteComputeWorld() {
        Debug.Log("ExecuteComputeWorld");
        var vc = VoroCompute.Instance;
        //vc.VerifyEditorDiagrams(); // prints the editor contents to a string

        for (var x = 0; x < width; x++) {
            for (var z = 0; z < height; z++) {
                vc.Compute(ref _tileGrid.Tiles[x, z]);
            }
        }
    }
}
}