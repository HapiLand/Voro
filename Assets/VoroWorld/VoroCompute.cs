using System.Collections.Generic;
using VoroUI;
using VoroUI.EditorTabs;
using VoroUI.Effects.Base;

namespace VoroWorld {
/// <summary>
///     VoroCompute will carry out the Terrain Generation system
///     UI Editor contains elements that are what drives the generation
///     --
///     VoroCompute will gather the UI data into a format that the generation needs
/// </summary>
public class VoroCompute {
    readonly TileMap _tileMap;

    public VoroCompute(TileMap tileMap) {
        _tileMap = tileMap;
        EditorTab.OnEditorOutputToCompute += EditorControlValueChanged;
    }

    void EditorControlValueChanged(Dictionary<EditorResult, List<IEffect>> content) {
        // a EffectData value inside the editor was changed, VoroCompute must regenerate the terrain
        // to show the generation with the new value
        // Debug.Log("VoroCompute registered Editor change - Recompute");

        // for every layer, compute its effects
        foreach (var kvp in content) {
            if (kvp.Value != null && kvp.Value.Count > 0) {
                foreach (var effect in kvp.Value) {
                    // compute the tiles
                    ComputeEffectOnVoroDiagrams(effect);
                }
            }
        }

        void ComputeEffectOnVoroDiagrams(IEffect effect) {
            // access the VoroDiagrams
            var diagrams = _tileMap.GetDiagramMapArray();

            for (var x = 0; x < diagrams.GetLength(0); x++) {
                for (var z = 0; z < diagrams.GetLength(1); z++) {
                    // Debug.Log($"Compute VoroDiagram.Tile:{diagrams[x, z].Tile} with Effect {effect.Name}");
                    effect.Compute(ref diagrams[x, z]);
                }
            }
        }
    }
}
}

/*
     public void Compute(IEffect effect, ref WorldTile tile) {
       // compute the function of the effect
       effect.Compute(ref tile);
   }

     public void ExecuteComputeWorld(Dictionary<EditorDiagram, List<IEffect>> editorContent) {
       // Debug.Log("Executing VoroCompute on all tiles within TileGrid");

                {


               }
           }
       }
   }
*/