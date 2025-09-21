using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;
using VoroUI.EditorTabs.Base;
using VoroUI.EditorTabs.Nodes;

namespace VoroUI.EditorTabs.Layers {
public class LayersTab : WindowTab {
    readonly NodesTab _nodesTab;

    /// <summary>
    ///     collection to store the elements
    /// </summary>
    VisualElement _collection;

    Button _newLayerInfoButton;

    /// <summary>
    ///     the dictionary stores the content of the LayersTab
    ///     new Layers are added into this
    ///     new Nodes are added into the active Layer
    /// </summary>
    public Dictionary<LayerInfo, List<NodeInfo>> LayerDictionary;

    public LayersTab(out NodesTab nodesTab) {
        LayerDictionary = new Dictionary<LayerInfo, List<NodeInfo>>();

        // create NodesTab in order to access events
        _nodesTab = new NodesTab();
        _nodesTab.OnCreateNodeInfo += OnCreateNodeInfo;
        _nodesTab.OnNodeInfoActive += OnNodeInfoActive;
        nodesTab = _nodesTab;

        SetTabStyle();
        CreateTabElements();

        // handle event to produce a new LayerInfo
        _newLayerInfoButton.clicked += () => {
            // create a new LayerInfo
            OnCreateNewLayerInfo();
            // display the LayerInfo elements
            RefreshTab();
            // display the NodeInfo elements for the active LayerInfo
            RefreshNodeTab();
        };

        return;

        void SetTabStyle() {
            style.flexDirection = FlexDirection.Column; // vertical layout
            style.flexGrow = 1; // full size
        }

        void CreateTabElements() {
            // heading
            TabHeading = new Label("Layers");
            Add(TabHeading);

            // collection of LayerInfo elements
            _collection = new VisualElement();
            Add(_collection);

            // button to create a new LayerInfo entry
            _newLayerInfoButton = new Button();
            _newLayerInfoButton.text = "New Layer";
            Add(_newLayerInfoButton);
        }
    }

    void OnCreateNewLayerInfo() {
        // create a new LayerInfo
        var layerName = $"Layer #{Random.Range(0, 999)}";
        var info = new LayerInfo(layerName);
        // add to dictionary
        LayerDictionary[info] = new List<NodeInfo>();

        // handle event when the LayerInfo is selected
        info.OnActive += () => {
            // only a single LayerInfo can be set as active
            // deselect any other LayerInfo if they are currently active
            foreach (var kv in LayerDictionary) {
                var other = kv.Key;
                if (other != info && other.Active) {
                    other.Active = false;
                    other.Element.Active = false;
                }
            }

            // display the NodeInfo elements for the active LayerInfo
            RefreshNodeTab();
        };
    }

    void RefreshTab() {
        // clear existing elements
        _collection.Clear();

        // display the LayerInfo elements
        foreach (var kv in LayerDictionary) {
            _collection.Add(kv.Key.Element);
        }
    }

    #region Nodes

    /// <summary>
    ///     called when NodeInfo is created
    ///     it is added to the active layer in the dictionary
    /// </summary>
    /// <param name="info"></param>
    void OnCreateNodeInfo(NodeInfo info) {
        // find if any LayerInfo are active
        var activeLayer = LayerDictionary.Keys.FirstOrDefault(k => k.Active);
        if (activeLayer == null) {
            Debug.LogError("no active LayerInfo in dictionary");
            return;
        }

        // add the info to the active layer
        LayerDictionary[activeLayer].Add(info);

        // display the NodeInfo elements
        RefreshNodeTab();
    }

    /// <summary>
    ///     called when a new NodeInfo is created
    ///     also called when the active LayerInfo changes
    ///     the NodeElements are displayed
    /// </summary>
    void RefreshNodeTab() {
        _nodesTab.ClearElements();
        
        // find if any LayerInfo are active
        var activeLayer = LayerDictionary.Keys.FirstOrDefault(k => k.Active);
        if (activeLayer == null) {
            Debug.LogError("no active LayerInfo in dictionary");
            return;
        }

        // get all elements from the active layer
        foreach (var nodeInfo in LayerDictionary[activeLayer]) {
            _nodesTab.AddElement(nodeInfo.Element);
        }
    }

    /// <summary>
    ///     called when the NodeInfo is selected
    ///     the NodeElement is updated
    /// </summary>
    /// <param name="info"></param>
    void OnNodeInfoActive(NodeInfo info) {
        
        // find if any LayerInfo are active
        var activeLayer = LayerDictionary.Keys.FirstOrDefault(k => k.Active);
        if (activeLayer == null) {
            Debug.LogError("no active LayerInfo in dictionary");
            return;
        }
        
        // only a single NodeInfo can be set as active
        // deselect any other NodeInfo if they are currently active
        foreach (var other in LayerDictionary[activeLayer]) {
            if (other != info && other.Active) {
                other.Active = false;
                other.Element.Active = false;
            }
        }
        
        // display the NodeInfo elements for the active LayerInfo
        RefreshNodeTab();
    }

    #endregion
}
}