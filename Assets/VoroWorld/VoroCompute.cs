using System.Collections.Generic;
using UnityEngine;
using VoroUI;
using VoroUI.Effects;

namespace VoroWorld {
/// <summary>
///     VoroCompute will carry out the Terrain Generation system
///     UI Editor contains elements that are what drives the generation
///     --
///     VoroCompute will gather the UI data into a format that the generation needs
/// </summary>
public class VoroCompute {
    public VoroCompute() {
        EditorWindow.OnEditorOutputToCompute += EditorControlValueChanged;
    }

    void EditorControlValueChanged(Dictionary<EditorDiagram, List<IEffect>> content) {
        // a EffectData value inside the editor was changed, VoroCompute must regenerate the terrain
        // to show the generation with the new value
        Debug.Log("VoroCompute registered Editor change - Recompute");
    }
}
}

/*
     public void Compute(IEffect effect, ref WorldTile tile) {
       // compute the function of the effect
       effect.Compute(ref tile);
   }



   };

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
*/