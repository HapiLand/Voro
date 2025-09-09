using UnityEngine;
using UnityEngine.UIElements;
using VoroEditor.Source;

namespace VoroEditor.Utility {
/// <summary>
///     reads/writes the EffectData of a selected node
///     will display the contents in the editor GUI
/// </summary>
public class InspectorManager {
    /// <summary>
    ///     this is where to add the display element
    /// </summary>
    readonly VisualElement _inspectorContainer;

    /// <summary>
    ///     this is the current visual element
    ///     which is displaying the contents of the selected effect
    /// </summary>
    VisualElement _display;

    public InspectorManager(VisualElement container) {
        _inspectorContainer = container;

        // subscribe to the selection events in node
        // this tells the inspector when to display the effect
        Node.OnNodeSelectedEvent += OnNodeSelected;
        Node.OnNodeUnselectedEvent += OnNodeUnselected;
    }

    /// <summary>
    ///     called when the user toggles a node to select it
    ///     this tells the inspector that it should display this nodes effect data
    /// </summary>
    /// <param name="nodeElement">the node which was selected</param>
    public void OnNodeSelected(VisualElement nodeElement) {
        Debug.Log($"Node Selected {nodeElement}");
        AddDisplay(nodeElement as Node);
    }

    /// <summary>
    ///     called when the user toggles deselects a node
    ///     this will remove the display of that effect
    /// </summary>
    public void OnNodeUnselected() {
        Debug.Log("Node Unselected");
        RemoveDisplay();
    }

    /// <summary>
    ///     adds the element that displays the contents of the EffectData
    /// </summary>
    /// <param name="selectedNode">the node which the user has selected</param>
    void AddDisplay(Node selectedNode) {
        Debug.Log($"AddDisplay effect: {selectedNode.Effect}");

        // stop duplicate displays from being possible
        RemoveDisplay();

        // get the display element from the Node
        // this display contains the elements for the user to interact with
        // ToDo display the value of the sliders within the display to indicate the actual slider value
        _display = selectedNode.Effect.Display;

        // add to hierarchy
        _inspectorContainer.Add(_display);
    }

    /// <summary>
    ///     removes the display from the inspector hierarchy
    /// </summary>
    void RemoveDisplay() {
        if (_display is { parent: not null }) {
            _display.RemoveFromHierarchy();
        }

        _display = null;
    }
}
}