using System;
using System.Linq;
using EditorGUI.Elements;
using EditorGUI.Panels;
using EditorGUI.Source.Voro.Grids;
using UnityEngine;

namespace EditorGUI.Source.Voro {
public class VoroCompute {
    readonly EditorPanel _editor;

    VoroCompute(EditorPanel editor) {
        _editor = editor;
        Console.WriteLine("New VoroCompute constructed");
    }

    public static VoroCompute Instance { get; private set; }

    public static void Initialize(EditorPanel editor) {
        if (Instance != null) {
            throw new InvalidOperationException("VoroCompute already initialised");
        }

        Instance = new VoroCompute(editor);
    }

    public void Compute(ref WorldTile tile) {
        // access each layer that the editor has
        var layers = _editor.EditorLayers;

        // compute each layer from the editor
        foreach (var kvp in layers) {
            var layer = kvp.Key; // this layer
            var effects = kvp.Value; // all effects within the layer

            foreach (var effect in effects) {
                // compute the effects within this layer
                ComputeEffect(effect, ref tile);
            }
        }

        return;

        void ComputeEffect(EffectElement effect, ref WorldTile tile) {
            // compute the effect which will modify the diagram
            effect.EffectInstance.Compute(ref tile);
        }
    }

    /// <summary>
    ///     Checks to find what DiagramElements are in use by the Editor
    /// </summary>
    public void VerifyEditorDiagrams() {
        var layers = _editor.EditorLayers;
        Debug.Log($"Editor contains {layers.Count()} Layers");

        // create a verbose output to display the layers contents in full
        foreach (var kvp in layers) {
            var layer = kvp.Key;
            var effects = kvp.Value;

            Debug.Log($"Layer: {layer.DisplayName}");

            foreach (var effect in effects) {
                Debug.Log($"    Effect: {effect.DisplayName}");
            }
        }
    }
}
}