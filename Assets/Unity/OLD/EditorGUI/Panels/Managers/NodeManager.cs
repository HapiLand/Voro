using System.Collections.Generic;
using EditorGUI.Elements;
using UnityEngine;
using UnityEngine.UIElements;

namespace EditorGUI.Panels.Managers {
public class NodeManager {
    readonly VisualElement _nodeContainer;

    public NodeManager(VisualElement container) {
        _nodeContainer = container;

        DiagramElement.OnDiagramSelectedEvent += OnDiagramSelected;
        DiagramElement.OnNoSelectedDiagrams += OnNoSelectedDiagrams;
    }

    public IEnumerable<NodeElement> NodeElements {
        get
        {
            foreach (var child in _nodeContainer.Children()) {
                if (child is NodeElement element) {
                    yield return element;
                }
            }
        }
    }

    /// <summary>
    ///     a diagram has been selected
    /// </summary>
    /// <param name="element"></param>
    void OnDiagramSelected(DiagramElement diagram) {
        // remove any nodes as the selected element will replace the contents
        RemoveAllNodes();

        // the element might contain nodes, if so, add them to the GUI
        foreach (var node in diagram.NodeInstances) {
            _nodeContainer.Add(node);
        }
    }

    void RemoveAllNodes() {
        _nodeContainer.Clear();
    }

    /// <summary>
    ///     no diagrams are currently selected
    /// </summary>
    void OnNoSelectedDiagrams() {
        RemoveAllNodes();
    }

    /// <summary>
    ///     called when the "NewNode" button is pressed to add a new node into the selected diagram
    /// </summary>
    public void AddNewNode() {
        Debug.Log("Add New Node pressed");
    }

    /*public event Action<string> OnNodeCreated;

    public NodeElement CreateNodeElement(string name, Action<NodeElement> onClicked = null) {
        var element = new NodeElement
        {
            DisplayName = name,
            name = name
        };

        element.clicked += () => {
            Debug.Log($"{name} Selected");
            // notify any external listeners
            OnNodeCreated?.Invoke(name);
            // callback to the LayerList frame
            onClicked?.Invoke(element);
        };
        return element;
    }*/
}
}