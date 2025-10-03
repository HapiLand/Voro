using System;
using System.Collections.Generic;

namespace Voro.UI.Internal {
class EditorDiagram {
    public readonly List<Layer> Layers = new();
    Node _activeNode;

    public Layer ActiveLayerElement { get; private set; }

    public bool CheckForActiveLayer() {
        return ActiveLayerElement != null;
    }

    public Layer CreateNewLayer() {
        var layer = new Layer();
        layer.Clicked += OnLayerClicked;
        layer.Selected += sel => ActiveLayerElement = sel;

        Layers.Add(layer);
        return layer;
    }

    void OnLayerClicked(Layer clickedLayer) {
        if (clickedLayer.Active) {
            clickedLayer.SetInactive();
            ActiveLayerElement = null;
        }
        else {
            foreach (var layer in Layers) {
                if (layer == clickedLayer) {
                    layer.SetActive();
                    ActiveLayerElement = layer;
                }
                else {
                    layer.SetInactive();
                }
            }
        }
    }

    public Node CreateNewNode() {
        if (ActiveLayerElement == null) {
            throw new InvalidOperationException("No active layer");
        }

        var node = new Node();
        node.Clicked += OnNodeClicked;
        node.Selected += sel => _activeNode = sel;

        ActiveLayerElement.Nodes.Add(node);
        return node;
    }

    void OnNodeClicked(Node clickedNode) {
        if (ActiveLayerElement == null) {
            return;
        }

        if (clickedNode.Active) {
            clickedNode.SetInactive();
            if (_activeNode == clickedNode) {
                _activeNode = null;
            }
        }
        else {
            foreach (var node in ActiveLayerElement.Nodes) {
                if (node == clickedNode) {
                    node.SetActive();
                    _activeNode = node;
                }
                else {
                    node.SetInactive();
                }
            }
        }
    }
}
}