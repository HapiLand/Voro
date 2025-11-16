using UnityEngine;
using VoroSystem.Designer.GraphSystem;
using VoroSystem.Landscape.TilemapSystem;
using VoroSystem.Landscape.TilemapSystem.Maps.Chunk;

namespace VoroSystem.Generation.DiagramSystem {
[ExecuteInEditMode]
[RequireComponent(typeof(DesignerComponent))]
[RequireComponent(typeof(TilemapComponent))]
public class DiagramComponent : MonoBehaviour {
    DesignerComponent designer;
    TilemapComponent tilemap;
    public static DiagramComponent Instance { get; private set; }

    #region Event Functions

    void Awake() {
        if (Instance != null) {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        designer = DesignerComponent.Instance;
        tilemap = TilemapComponent.Instance;
    }

    #endregion

    public void RunEffects(ChunkTilemap tilemap) {
        designer.GetGraphDictionary(out var design);
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