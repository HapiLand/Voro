using System.Collections.Generic;
using UnityEngine;
using VoroUI;
using VoroUI.Effects;
using VoroWorld.Grids;

namespace VoroWorld {
/// <summary>
///     manages the Unity GameWorld Scene, computes values from the Editor to generate the actual game scene
/// </summary>
[ExecuteAlways]
public class GameWorldManager : MonoBehaviour {
    TileGrid _tileGrid;

    VoroCompute _voroCompute;
    int width => 5;
    int height => 1;

    void Awake() {
        // create VoroCompute so the Terrain can be generated
        _voroCompute = new VoroCompute();

        // clear any pre-existing children
        var numChildren = transform.childCount;
        for (var i = numChildren - 1; i >= 0; i--) {
            DestroyImmediate(transform.GetChild(i).gameObject);
        }

        EditorWindow.OnEditorOutputToCompute += o => {
            // Debug.Log($"EditorWindow UI initiating the VoroCompute in GameWorldManager - Received {o}");
            ExecuteComputeWorld(o);
        };
        InitializeWorldGrid();
    }

    void InitializeWorldGrid() {
        _tileGrid = new TileGrid(width, height, transform);
    }

    public WorldTile GetTile(int x, int z) {
        return _tileGrid?.GetTile(x, z);
    }

    public void ExecuteComputeWorld(Dictionary<EditorDiagram, List<IEffect>> editorContent) {
        // Debug.Log("Executing VoroCompute on all tiles within TileGrid");

        // in every layer, for each effect within the layer
        // compute that effect on every tile
        foreach (var kvp in editorContent) {
            if (kvp.Value != null && kvp.Value.Count > 0) {
                foreach (var effect in kvp.Value) {
                    // compute the tiles
                    for (var x = 0; x < width; x++) {
                        for (var z = 0; z < height; z++) {
                            _voroCompute.Compute(effect, ref _tileGrid.Tiles[x, z]);
                        }
                    }
                }
            }
        }
    }
}
}