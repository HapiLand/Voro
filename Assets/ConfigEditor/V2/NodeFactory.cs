using UnityEngine;
using UnityEngine.UIElements;

namespace ConfigEditor.V2 {
public class NodeFactory {
    VisualElement _selectedNode;

    /// <summary>
    ///     creates a new effect
    /// </summary>
    /// <param name="effectName">the name of the effect</param>
    /// <returns>the visual element of the node effect</returns>
    public Node Create(string effectName) {
        var node = Node.CreateInstance(effectName);
        node.name = $"Node_{effectName}";
        node.AddToClassList("node");

        // horizontal container for the header of the node
        var header = new VisualElement();
        header.AddToClassList("node-row");

        // display name of the node
        var label = new Label(effectName);
        label.AddToClassList("node-label");

        // toggle allows the node to be bypassed
        var toggle = new Toggle();
        toggle.AddToClassList("node-toggle");

        // register when a node is selected, same idea as selecting a column
        toggle.RegisterValueChangedCallback(evt => {
            if (evt.newValue) {
                SelectNode(node);
            }
            else if (_selectedNode == node) {
                _selectedNode = null;
                node.RemoveFromClassList("selected-node");
            }
        });

        header.Add(toggle);
        header.Add(label);
        node.Add(header);

        // horizontal container for the buttons
        var buttonRow = new VisualElement();
        buttonRow.AddToClassList("node-row");

        // create the delete button so the node can be removed
        // reset selections
        var deleteBtn = new Button(() => {
            if (_selectedNode == node) {
                _selectedNode = null;
            }

            node.RemoveFromHierarchy();
        })
        {
            text = "X"
        };

        // create the up and down buttons that will change the order of this node in the container
        var upBtn = new Button(() => MoveNode(node, -1)) { text = "↑" };
        var downBtn = new Button(() => MoveNode(node, 1)) { text = "↓" };

        foreach (var btn in new[] { deleteBtn, upBtn, downBtn }) {
            btn.AddToClassList("node-button");
        }

        buttonRow.Add(deleteBtn);
        buttonRow.Add(upBtn);
        buttonRow.Add(downBtn);

        node.Add(buttonRow);
        return node;
    }

    /// <summary>
    ///     allow a node to be selected via its toggle, same use as Column
    /// </summary>
    /// <param name="nodeToSelect"></param>
    void SelectNode(VisualElement nodeToSelect) {
        if (_selectedNode != null && _selectedNode != nodeToSelect) {
            _selectedNode.RemoveFromClassList("selected-node");
            var previousToggle = _selectedNode.Q<Toggle>();
            if (previousToggle != null) {
                previousToggle.SetValueWithoutNotify(false);
            }
        }

        _selectedNode = nodeToSelect;
        _selectedNode.AddToClassList("selected-node");
    }

    /// <summary>
    ///     changes the position of the node in a up/down direction
    /// </summary>
    /// <param name="node"></param>
    /// <param name="direction"></param>
    void MoveNode(VisualElement node, int direction) {
        // get the parent of this node, which is a column
        var parent = node.parent;
        if (parent == null) {
            return;
        }

        // find the current index and the new index that is in the desired direction
        var index = parent.IndexOf(node);
        var newIndex = Mathf.Clamp(index + direction, 0, parent.childCount - 1);
        if (index == newIndex) {
            return;
        }

        // update order of nodes
        parent.Remove(node);
        parent.Insert(newIndex, node);
    }
}
}