using System;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;
using VoroUI.Effects;
using VoroUI.Elements;

namespace VoroUI.EditorTabs {
public class NodesTab : VisualElement {
    readonly VisualElement _collection;

    public NodesTab() {
        style.flexDirection = FlexDirection.Column; // vertical layout
        style.flexGrow = 1; // full size
        // heading
        Add(new Label("Effects"));
        // collection of effect elements
        _collection = new VisualElement();
        Add(_collection);
        // button to create a new Effect Element
        var newEffectBtn = new Button();
        newEffectBtn.text = "New Effect";
        Add(newEffectBtn);
        // hide button when no layers are active
        newEffectBtn.style.display = DisplayStyle.None;
        LayersTab.AnyLayersActive += isActive => {
            newEffectBtn.style.display = isActive ? DisplayStyle.Flex : DisplayStyle.None;
        };

        // to refresh the effects UI
        LayersTab.OnLayerSelectionChanged += RefreshEffectsUI;

        newEffectBtn.clicked += () => {
            if (!LayersTab.CheckAnyActive()) {
                // this is a redundant check, the button is only visible and clickable
                // when the layer is active, theres no real need to check again that
                // a layer is selected
                return;
            }

            // create new effect
            // todo pick the desired effect from a dropdown menu
            var effect = new DefaultFX(); // IEffect

            // create new element to store the effect
            var element = new Node(effect);
            OnEffectElementCreated?.Invoke(element);

            // the element is selected when clicked
            element.Clicked += () => {
                Select(element);
                AnyEffectsActive?.Invoke(CheckForAnyActive());
            };

            // add Effect to vertical list
            _collection.Add(element);
        };

        // when a layer is clicked, to select it, the effects that are shown
        // should be cleared
        LayersTab.OnLayerSelectionChanged += RefreshEffectsUI;

        EditorEvents.OnSceneReloaded += OnSceneReloaded;
    }

    void OnSceneReloaded() {
        // clear the child content
        _collection.Clear();
    }

    public static event Action<bool> AnyEffectsActive;

    public bool CheckForAnyActive() {
        var result = TryGetActiveElement(out var effectElement);
        if (result) {
            return true;
        }

        // no layers are active
        Debug.LogWarning("no active effects found");
        return false;
    }

    /// <summary>
    /// </summary>
    public static event Action<Node> OnEffectSelectionChanged;

    public bool TryGetActiveElement(out Node? activeEffect) {
        var effectElements = _collection.Children().OfType<Node>();

        foreach (var element in effectElements) {
            if (element.Active) {
                activeEffect = element;
                return true;
            }
        }

        // no effects are active
        activeEffect = null;
        return false;
    }

    void Select(Node clickedElement) {
        var nothingSelected = !TryGetActiveElement(out var activeElement);
        if (nothingSelected) {
            // no effect is currently selected, so this element is the new selection

            // when the selection changes to a different element than the previous selection
            clickedElement.SetActive();
            OnEffectSelectionChanged?.Invoke(clickedElement); // the active value in an element has been changed
            return;
        }

        // an element is already selected
        if (activeElement == clickedElement) {
            // the element which is already active, as clicked
            // this deselects it, like a toggle
            clickedElement.SetInactive();
            OnEffectSelectionChanged?.Invoke(clickedElement); // the active value in an element has been changed
            return;
        }

        // a different element was clicked than the already active elemnet
        // this changes the selection
        activeElement.SetInactive(); // old element is inactive
        clickedElement.SetActive(); // new element is active
        OnEffectSelectionChanged?.Invoke(clickedElement); // the active value in an element has been changed
    }

    /// <summary>
    ///     when the layer selection changes, the effects from the new layer must be displayed
    /// </summary>
    void RefreshEffectsUI(Layer layer) {
        // clear any existing elements
        _collection.Clear();
        // display the new effects within the layer
        foreach (var effectElement in layer.EditorResukt.EffectElements) {
            _collection.Add(effectElement);
        }
    }

    /// <summary>
    ///     when a new effect button is clicked, IEffect is created and then
    ///     stored inside an EffectElement
    ///     the element will be given to the active Layer
    /// </summary>
    public static event Action<Node> OnEffectElementCreated;
}
}