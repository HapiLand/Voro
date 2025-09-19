using System;
using UnityEngine.UIElements;

namespace VoroUI {
public class LayersTab : VisualElement {
    public readonly VisualElement Collection;

    public LayersTab() {
        style.flexDirection = FlexDirection.Column;
        // heading
        Add(new Label("Layers"));
        // collection of layer elements
        // each element contains a Layer
        Collection = new VisualElement();
        Add(Collection);
        // button to create a new Layer Element
        var newLayerBtn = new Button();
        newLayerBtn.text = "New Layer";
        Add(newLayerBtn);

        // create a layer and an element when the button is clicked
        // invoke events that update the UI and the generation system
        newLayerBtn.clicked += () => {
            var layerName = "NewLayer";
            // create new layer
            var layer = new Layer(layerName);
            OnLayerCreated?.Invoke(layer);
            // create new element to store the layer
            var element = new LayerElement(layer);
            // the element is selected when clicked
            element.Clicked += () => {
                Select(element);
                OnLayerSelectionChanged.Invoke();
            };
            // add Layer to vertical list
            Collection.Add(element);
        };
        EffectsTab.OnEffectCreated += AddEffectToActiveLayer;
    }

    public static event Action OnLayerSelectionChanged;

    void AddEffectToActiveLayer(IEffect effect) {
        var fail = !TryGetActiveLayer(out var layer);
        if (fail) {
            // nothing selected
            return;
        }

        // add to the layer
        layer.AddEffect(effect);
    }

    public bool TryGetActiveLayer(out Layer? activeLayer) {
        // todo complete method
        //  find the LayerElement that is Active=true
        //  return element.Layer
        activeLayer = null;
        return false;
    }

    public static event Action<Layer> OnLayerCreated;

    void Select(LayerElement element) {
        // todo selectable elements, toggle
        // todo deslect other elements
        throw new NotImplementedException();
    }
}
}