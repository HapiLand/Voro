using System;
using UnityEngine.UIElements;
using VoroUI.EditorTabs.Base;
using VoroUI.EditorTabs.Layers;
using Random = UnityEngine.Random;

namespace VoroUI.EditorTabs.Nodes {
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
            OnCreateNewNodeInfo();
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

            // collection of NodeInfo elements
            _collection = new VisualElement();
            Add(_collection);

            // button to create a new NodeInfo entry
            _newNodeInfoButton = new Button();
            _newNodeInfoButton.text = "New Node";
            Add(_newNodeInfoButton);
        }
    }

    void OnCreateNewNodeInfo() {
        // create a new NodeInfo
        var nodeName = $"Node #{Random.Range(0, 999)}";
        var info = new NodeInfo(nodeName);
        // add to dictionary
        OnCreateNodeInfo?.Invoke(info);

        // handle event when the NodeInfo is selected
        info.OnActive += () => { OnNodeInfoActive?.Invoke(info); };
    }

    public event Action<NodeInfo> OnCreateNodeInfo;
    public event Action<NodeInfo> OnNodeInfoActive;

    public void ClearElements() {
        // clear existing elements
        _collection.Clear();
    }

    /// <summary>
    /// adds the node element to the collection to display it
    /// </summary>
    /// <param name="element"></param>
    public void AddElement(NodeElement element) {
        _collection.Add(element);
    }
}
}