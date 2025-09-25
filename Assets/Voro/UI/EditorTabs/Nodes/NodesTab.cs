using System;
using System.Collections.Generic;
using UnityEngine.UIElements;
using Voro.Jen.Compute.FX.Internal;
using Voro.UI.EditorTabs.Base;

namespace Voro.UI.EditorTabs.Nodes {
public class NodesTab : WindowTab {
    /// <summary>
    ///     collection to store the elements
    /// </summary>
    VisualElement _collection;

    Button _newNodeInfoButton;

    public NodesTab() {
        SetTabStyle();
        CreateTabElements();

        // handle event to produce a new NodeInfo
        _newNodeInfoButton.clicked += () => {
            // create a new NodeInfo
            var info = new NodeInfo(EffectName.ConstantHeight); // todo pick from menu
            info.OnActive += () => OnNodeSelected?.Invoke(info);
            OnNodeCreated?.Invoke(info);
        };
        return;

        void SetTabStyle() {
            style.flexDirection = FlexDirection.Column; // vertical layout
            style.flexGrow = 1; // full size
        }

        void CreateTabElements() {
            // heading
            TabHeading = new Label("Nodes");
            Add(TabHeading);

            // collection of LayerInfo elements
            _collection = new VisualElement();
            Add(_collection);

            // button to create a new LayerInfo entry
            _newNodeInfoButton = new Button();
            _newNodeInfoButton.text = "New Node";
            Add(_newNodeInfoButton);
        }
    }

    public event Action<NodeInfo> OnNodeCreated;
    public event Action<NodeInfo> OnNodeSelected;

    public void Refresh(IEnumerable<NodeInfo> nodes) {
        _collection.Clear();
        foreach (var node in nodes) {
            _collection.Add(node.Element);
        }
    }
}
}