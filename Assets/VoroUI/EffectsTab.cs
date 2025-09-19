using System;
using UnityEngine.UIElements;

namespace VoroUI {
public class EffectsTab : VisualElement {
    readonly VisualElement _collection;
    readonly LayersTab _layers;

    public EffectsTab(LayersTab layers) {
        // in order to find the active layer
        _layers = layers;
        style.flexDirection = FlexDirection.Column;
        // heading
        Add(new Label("Effects"));
        // collection of effect elements
        _collection = new VisualElement();
        Add(_collection);
        // button to create a new Effect Element
        var newEffectBtn = new Button();
        newEffectBtn.text = "New Effect";
        Add(newEffectBtn);
        // todo hide button when no layer is selected

        // assuming this button is only clicked
        // when a layer is currently active
        newEffectBtn.clicked += () => {
            var fail = !_layers.TryGetActiveLayer(out var layer);
            if (fail) {
                // nothing selected
                return;
            }

            // create a new effect and add it to the layer
            // todo pick the desired effect within UI
            var effect = new DefaultEffect();
            // callback so the effect can be added to the layer
            OnEffectCreated?.Invoke(effect);
            // create new element to store the effect
            var element = new EffectElement(effect);
            // the element is selected when clicked
            element.Clicked += () => { Select(element); };
            // add Effect to vertical list
            _collection.Add(element);
        };
        LayersTab.OnLayerSelectionChanged += ClearEffects;
    }

    /// <summary>
    /// </summary>
    public static event Action OnEffectSelectionChanged;

    public bool TryGetActiveEffect(out Effect? activeEffect) {
        // todo complete method
        //  find the LayerElement that is Active=true
        //  return element.Layer
        activeLayer = null;
        return false;
    }

    void Select(LayerElement element) {
        // todo selectable elements, toggle
        // todo deslect other elements
        throw new NotImplementedException();
    }

    /// <summary>
    ///     clears the visible effect elements as the active layer
    ///     has changed
    /// </summary>
    void ClearEffects() {
        _collection.Clear();
    }

    /// <summary>
    ///     the effect is added to the layer
    /// </summary>
    public static event Action<IEffect> OnEffectCreated;
}
}