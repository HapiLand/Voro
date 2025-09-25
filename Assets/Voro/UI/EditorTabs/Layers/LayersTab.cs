using System;
using System.Collections.Generic;
using UnityEngine.UIElements;
using Voro.UI.EditorTabs.Base;
using Random = UnityEngine.Random;

namespace Voro.UI.EditorTabs.Layers {
public class LayersTab : WindowTab {
    /// <summary>
    ///     collection to store the elements
    /// </summary>
    VisualElement _collection;

    Button _newLayerInfoButton;

    public LayersTab() {
        SetTabStyle();
        CreateTabElements();

        // handle event to produce a new LayerInfo
        _newLayerInfoButton.clicked += () => {
            // create a new LayerInfo
            var info = new LayerInfo($"Layer #{Random.Range(0, 999)}");
            info.OnActive += () => OnLayerSelected?.Invoke(info);
            OnLayerCreated?.Invoke(info);
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

    public event Action<LayerInfo> OnLayerCreated;
    public event Action<LayerInfo> OnLayerSelected;

    public void Refresh(IEnumerable<LayerInfo> layers) {
        _collection.Clear();
        foreach (var layer in layers) {
            _collection.Add(layer.Element);
        }
    }
}
}