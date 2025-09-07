using System;
using ConfigEditor.V2.Effects.Internal;
using UnityEngine;
using UnityEngine.UIElements;

namespace ConfigEditor.V2 {
public class Node : VisualElement {
    static Node _selectedNode;

    public IEffect2 Effect;
    // ToDo inspector must read/write the effect data value

    Node(IEffect2 effect) {
        // ToDo implement logic in Node that will execute an actual function for the nodes effect
        //  Node to be used like the IEffect interface in V1
        Effect = effect;
        Debug.Log($"node created with: {Effect}");

        // GUI
        name = $"Node_{Effect.EffectName}";
        AddToClassList("node");
        // create the layout of the node
        BuildUI();
    }

    public static event Action<Node> OnNodeSelectedEvent;
    public static event Action OnNodeUnselectedEvent;

    public static Node CreateInstance(string effectName) {
        // data which is the configuration for this effect
        // the inspector will display/alter these values
        // a new instance of the effect, which was cloned from the dictionary
        // this is what the visual element Node shall use

        // construct the visual element
        var node = new Node(EffectFactory.Create(effectName));

        return node;
    }

    void BuildUI() {
        // horizontal container for the header of the node
        var header = new VisualElement();
        header.AddToClassList("node-row");

        // display name of the node
        var label = new Label(Effect.EffectName);
        label.AddToClassList("node-label");

        // toggle allows the node to be bypassed
        var toggle = new Toggle();
        toggle.AddToClassList("node-toggle");
        // register when a node is selected, same idea as selecting a column
        toggle.RegisterValueChangedCallback(OnToggleValueChanged);

        // add to hierarchy
        header.Add(toggle);
        header.Add(label);
        Add(header);

        // horizontal container for the buttons
        var buttonRow = new VisualElement();
        buttonRow.AddToClassList("node-row");

        // create the delete button so the node can be removed
        // reset selections
        var deleteBtn = new Button(() => RemoveFromHierarchy()) { text = "X" };
        // create the up and down buttons that will change the order of this node in the container
        var upBtn = new Button(() => Move(-1)) { text = "↑" };
        var downBtn = new Button(() => Move(1)) { text = "↓" };

        // add to hierarchy
        foreach (var btn in new[] { deleteBtn, upBtn, downBtn }) {
            btn.AddToClassList("node-button");
        }

        buttonRow.Add(deleteBtn);
        buttonRow.Add(upBtn);
        buttonRow.Add(downBtn);
        Add(buttonRow);
    }

    void OnToggleValueChanged(ChangeEvent<bool> evt) {
        if (evt.newValue) {
            Select();
        }
        else if (_selectedNode == this) {
            _selectedNode = null;
            RemoveFromClassList("selected-node");
            // unselection event so the inspector will no longer display this node
            OnNodeUnselectedEvent?.Invoke();
        }
    }

    /// <summary>
    ///     allow a node to be selected via its toggle, same as Column
    /// </summary>
    void Select() {
        if (_selectedNode != null && _selectedNode != this) {
            _selectedNode.RemoveFromClassList("selected-node");
            var previousToggle = _selectedNode.Q<Toggle>();
            if (previousToggle != null) {
                previousToggle.SetValueWithoutNotify(false);
            }

            // unselection event so the inspector will no longer display this node
            // OnNodeUnselectedEvent?.Invoke();
        }

        _selectedNode = this;
        AddToClassList("selected-node");

        Debug.Log($"node {_selectedNode.Effect.EffectName} was selected");
        // notify inspector that it shall display this effect
        OnNodeSelectedEvent?.Invoke(this);
    }

    /// <summary>
    ///     changes the position of the node in a up/down direction
    /// </summary>
    /// <param name="node"></param>
    /// <param name="direction"></param>
    void Move(int direction) {
        // get the parent of this node, which is a column
        var parent = this.parent;
        if (parent == null) {
            return;
        }

        // find the current index and the new index that is in the desired direction

        var index = parent.IndexOf(this);
        var newIndex = Mathf.Clamp(index + direction, 0, parent.childCount - 1);
        if (index == newIndex) {
            return;
        }

        // update order of nodes
        parent.Remove(this);
        parent.Insert(newIndex, this);
    }
}
}