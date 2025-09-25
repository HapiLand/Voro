using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Voro.Jen.Compute.FX.Base;
using Voro.Jen.Compute.FX.Internal;
using Voro.UI.EditorTabs.Layers;
using Voro.UI.EditorTabs.Nodes;

namespace Voro.UI.EditorTabs {
public class LayersNodesController {
    /// <summary>
    ///     the dictionary stores the Layer and Node info
    ///     VoroCompute generates terrain with this
    /// </summary>
    public Dictionary<LayerInfo, List<NodeInfo>> LayerDictionary = new();

    /// <summary>
    ///     LayersTab handles the Layer UI
    /// </summary>
    public LayersTab LayersTab;

    /// <summary>
    ///     NodesTab handles the Node UI
    /// </summary>
    public NodesTab NodesTab;

    public LayersNodesController(LayersTab layersTab, NodesTab nodesTab) {
        LayersTab = layersTab;
        NodesTab = nodesTab;

        // store the new layer in the dictionary
        LayersTab.OnLayerCreated += OnLayerCreated;
        // set layer as active when selected
        LayersTab.OnLayerSelected += OnLayerSelected;

        // store the new node with the active layer
        NodesTab.OnNodeCreated += OnNodeCreated;
        // set node as active when selected
        // this displays its control elements
        NodesTab.OnNodeSelected += OnNodeSelected;
    }

    void OnLayerCreated(LayerInfo info) {
        LayerDictionary[info] = new List<NodeInfo>();
        LayersTab.Refresh(LayerDictionary.Keys);
    }

    void OnLayerSelected(LayerInfo selected) {
        foreach (var layer in LayerDictionary.Keys) {
            layer.Active = layer == selected;
            layer.Element.Active = layer == selected;
        }

        NodesTab.Refresh(LayerDictionary[selected]);
    }

    void OnNodeCreated(NodeInfo info) {
        var activeLayer = LayerDictionary.Keys.FirstOrDefault(k => k.Active);
        if (activeLayer == null) {
            // Debug.LogError("no active layer");
            return;
        }

        // execute terrain generation when a control data value is changed
        info.OnValueChanged += RecomputeTerrain;

        LayerDictionary[activeLayer].Add(info);
        NodesTab.Refresh(LayerDictionary[activeLayer]);
    }

    /// <summary>
    ///     converts the dictionary into one with types that the WorldManager accepts
    /// </summary>
    void RecomputeTerrain() {
        Debug.Log($"To Recompute Terrain Now");
        
        // convert  Dictionary<LayerInfo, List<NodeInfo>>
        // into     Dictionary<string,    List<IEffect>>
        Dictionary<string, List<IEffect>> effectDictionary = new();

        foreach (var kvp in LayerDictionary) {
            // write name of LayerInfo to the dictionary
            var layerName = kvp.Key.Name;
            effectDictionary[layerName] = new List<IEffect>();

            if (kvp.Value.Count > 0) {
                foreach (var node in kvp.Value) {

                    var effect = EffectHelper.Create(node);
                    
                    effectDictionary[layerName].Add(effect);
                }
            }
        }

        OnRecompute?.Invoke(effectDictionary);
    }

    /// <summary>
    ///     called whenever the world must be updated
    ///     used to recompute the terrain after a Control Data value changes
    /// </summary>
    public static event Action<Dictionary<string, List<IEffect>>> OnRecompute;

    void OnNodeSelected(NodeInfo selected) {
        var activeLayer = LayerDictionary.Keys.FirstOrDefault(k => k.Active);
        if (activeLayer == null) {
            // Debug.LogError("no active layer");
            return;
        }

        // set the node as active, display the controls for it
        foreach (var node in LayerDictionary[activeLayer]) {
            node.Active = node == selected;
            node.Element.Active = node == selected;
        }

        NodesTab.Refresh(LayerDictionary[activeLayer]);
    }
}
}