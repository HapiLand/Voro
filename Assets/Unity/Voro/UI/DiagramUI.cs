using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;
using Voro.UI.Internal;

namespace Voro.UI {
public class DiagramUI : VisualElement {
    readonly EditorDiagram _diagram;
    readonly Button _makeDiagramButton;
    readonly Button _newLayerButton;
    readonly Button _newNodeButton;

    public DiagramUI() {
        _diagram = new EditorDiagram();

        _makeDiagramButton = new Button { text = "Make Diagram" };
        Add(_makeDiagramButton);
        _makeDiagramButton.clicked += () => {
            var diagramDict = Diagram;
            foreach (var kvp in diagramDict) {
                Debug.Log($"Layer: {kvp.Key.Name}");
                foreach (var nodeData in kvp.Value) {
                    Debug.Log($"  Node: {nodeData.Name}");
                }
            }
        };

        _newLayerButton = new Button { text = "New Layer" };
        Add(_newLayerButton);
        _newLayerButton.clicked += () => {
            var layer = _diagram.CreateNewLayer();
            Add(layer); // add Layer to UI
        };

        _newNodeButton = new Button { text = "New Node" };
        Add(_newNodeButton);
        _newNodeButton.clicked += () => {
            if (!_diagram.CheckForActiveLayer()) {
                return;
            }

            var node = _diagram.CreateNewNode();
            _diagram.ActiveLayerElement.Add(node); // add Node to active Layer's UI container
        };
    }

    Dictionary<LayerData, List<NodeData>> Diagram {
        get
        {
            var result = new Dictionary<LayerData, List<NodeData>>();

            foreach (var layer in _diagram.Layers) {
                var layerData = new LayerData(layer.DisplayName);
                result[layerData] = layer.Nodes
                    .Select(node => new NodeData(node.DisplayName))
                    .ToList();
            }

            return result;
        }
    }
}
}