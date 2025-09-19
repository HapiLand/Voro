using System;
using UnityEngine.UIElements;

namespace VoroUI {
public class LayerElement : VisualElement {
    /// <summary>
    ///     this is the current selection
    /// </summary>
    public bool Active;

    public Layer Layer;

    // todo move element in collection
    // todo remove element from collection
    public LayerElement(Layer layer) {
        Layer = layer;
        // label
        Add(new Label(layer.Name));
        // make element selectable
        var clickable = new Clickable(OnClicked);
        this.AddManipulator(clickable);
    }

    public event Action Clicked;

    void OnClicked() {
        Clicked?.Invoke();
    }
}
}