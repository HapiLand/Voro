using System;
using OLD.GUI.Elements;
using UnityEngine;

namespace OLD.GUI.Managers {
public class LayerManager {
    int _layerCounter = 1;

    public event Action<string> OnLayerCreated;

    public LayerItemElement CreateLayerItem(Action<LayerItemElement> onClicked = null) {
        var layerName = $"Layer {_layerCounter++}";

        var layerButton = new LayerItemElement { DisplayName = layerName };
        layerButton.clicked += () => {
            Debug.Log($"{layerName} clicked");

            // notify any external listeners
            OnLayerCreated?.Invoke(layerName);

            // callback to the LayerList frame
            onClicked?.Invoke(layerButton);
        };

        return layerButton;
    }
}
}