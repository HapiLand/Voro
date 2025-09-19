using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UIElements;

namespace VoroUI {
public class EditorWindow : VisualElement {
    LayersTab _layersTab;
    EffectsTab _effectsTab;
    
    public EditorWindow() {
        // create components
        _layersTab = new LayersTab();
        _effectsTab = new EffectsTab(_layersTab);
        var cam = new CameraTab();
        // left vertical layout
        var ve = new VisualElement();
        ve.style.flexDirection = FlexDirection.Column; // vertical layout
        ve.style.flexGrow = 1; // full size
        ve.Add(_layersTab);
        ve.Add(_effectsTab);
        // full layout
        style.flexDirection = FlexDirection.Row; // horizontal layout
        style.flexGrow = 1; // full size
        Add(ve);
        Add(cam);

        // events
        EffectBase.OnControlValueChanged += OnControlValueChanged;
    }

    // /// <summary>
    // /// call when the unity scene has been reloaded, the editor window must be cleared to reset it
    // /// </summary>
    // void OnSceneReloaded() {
    //     // clear the editor tabs to remove all the child content
    //     _layersTab.Clear();
    //     _effectsTab.Clear();
    //     // recreate the tabs
    //     _layersTab = new LayersTab();
    //     _effectsTab = new EffectsTab(_layersTab);
    // }
    
    /// <summary>
    ///     this method is called before the terrain generation system recomputes the result
    ///     any time the control UI changes a data value, VoroCompute needs the terrain to
    ///     show what the new value does to the terrain
    /// </summary>
    /// <param name="effect"></param>
    /// <param name="value"></param>
    void OnControlValueChanged(IEffect effect, object value) {
        // Debug.Log($"EffectData value changed in effect {effect.Name} new value = {value}");

        // EditorWindow will get the data from within the UI tabs and constructs a new object from it
        // the object is provided to VoroCompute, the result of the object produces the full terrain

        // turn the content of the editor into a dictionary which VoroCompute needs to generate terrain
        Dictionary<Layer, List<IEffect>> editorContent = new();

        // create the keys as the Layers found in the editor
        var layerElements = _layersTab.Query<LayerElement>().ToList();
        foreach (var layerElement in layerElements) {
            // store this layer and get the effect elements inside it
            var layer = layerElement.Layer;
            editorContent[layer] = new List<IEffect>();
            // store every effect within this layer
            foreach (var effectElement in layer.EffectElements) {
                editorContent[layer].Add(effectElement.Effect);
            }
        }

        // debug the dictionary content
        // LogDictionary(editorContent);

        OnEditorOutputToCompute?.Invoke(editorContent);

        void LogDictionary(Dictionary<Layer, List<IEffect>> dict) {
            Debug.Log("EditorWindow constructed the EditorContent dictionary");
            var sb = new StringBuilder();
            sb.AppendLine("EditorContent Dictionary:");
            foreach (var kvp in dict) {
                var layerName = kvp.Key != null ? kvp.Key.Name : "(null)";
                sb.AppendLine($"- Layer: {layerName}");

                if (kvp.Value != null && kvp.Value.Count > 0) {
                    foreach (var effect in kvp.Value) {
                        var effectName = effect != null ? effect.Name : "(null)";
                        sb.AppendLine($"   - Effect: {effectName}");
                    }
                }
                else {
                    sb.AppendLine("   (no effects)");
                }
            }

            Debug.Log(sb.ToString());
        }
    }

    public static event Action<Dictionary<Layer, List<IEffect>>> OnEditorOutputToCompute;
}
}