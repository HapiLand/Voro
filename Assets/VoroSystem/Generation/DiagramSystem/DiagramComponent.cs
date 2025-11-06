using UnityEngine;
using VoroSystem.Generation.GraphSystem;
using VoroSystem.Landscape.TilemapSystem;
using VoroSystem.Landscape.TilemapSystem.Maps.Chunk;

namespace VoroSystem.Generation.DiagramSystem {
[ExecuteInEditMode]
[RequireComponent(typeof(DesignerComponent))]
[RequireComponent(typeof(TilemapComponent))]
public class DiagramComponent : MonoBehaviour {
    DesignerComponent _designer;
    TilemapComponent _tilemap;
    public static DiagramComponent Instance { get; private set; }

    void Awake() {
        if (Instance != null) {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        _designer = DesignerComponent.Instance;
        _tilemap = TilemapComponent.Instance;
    }
    
    public void RunEffects(ChunkTilemap tilemap) {
        _designer.GetGraphDictionary(out var design);
        // for each layer
        foreach (var (layerName, effectManagers) in design) {
            Debug.Log($"running effects in layer {layerName}");
            // for each tile
            tilemap.ForEach(tile => {
                var br = tile.Result;
                // for each Effect Manager
                effectManagers.ForEach(fx => {
                    // compute
                    fx.RunEffect(br);
                });
            });
        }
    }
}
}