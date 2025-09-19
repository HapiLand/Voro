using System;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;
using VoroUI.Elements;

namespace VoroUI.EditorTabs {
public class LayersTab : VisualElement {
    public static VisualElement Collection;

    public LayersTab() {
        style.flexDirection = FlexDirection.Column; // vertical layout
        style.flexGrow = 1; // full size
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
            var element = ClickedNewLayerButton(layerName);

            // add Layer to vertical list
            Collection.Add(element);
        };
        EffectsTab.OnEffectElementCreated += AddEffectToActiveLayer;

        // set defaults
        SetInitialLayer();

        CameraTab.OnSceneReloaded += OnSceneReloaded;
    }

    public static bool CheckAnyActive() {
        var layerElements = Collection.Query<Layer>().ToList();
        return layerElements.Any(e => e.Active);
    }

    void OnSceneReloaded() {
        // clear the child content
        Collection.Clear();
    }


    /// <summary>
    ///     UI button to create a new Layer+LayerElement
    /// </summary>
    /// <param name="layerName"></param>
    /// <returns></returns>
    Layer ClickedNewLayerButton(string layerName) {
        // create new layer
        var layer = new EditorDiagram(layerName);
        OnLayerCreated?.Invoke(layer);

        // create new element to store the layer
        var element = new Layer(layer);

        // the element is selected when clicked
        element.Clicked += () => {
            Select(element);
            AnyLayersActive?.Invoke(CheckForAnyActive());
        };
        return element;
    }

    /// <summary>
    ///     the layer tab must always start with a layer already added
    /// </summary>
    void SetInitialLayer() {
        // a default layer is required, at least 1 layer is always needed
        var element = ClickedNewLayerButton("DefaultLayer");

        // add Layer to collection
        Collection.Add(element);
    }


    public static event Action<Layer> OnLayerSelectionChanged;

    void AddEffectToActiveLayer(Node node) {
        var fail = !TryGetActiveElement(out var layerElement);
        if (fail) {
            // nothing selected
            return;
        }

        // Debug.Log($"Add Effect to Active Layer : {effect.Name}");
        // add the effect to the layer
        layerElement.EditorDiagram.AddEffectElement(node);
    }

    public bool TryGetActiveElement(out Layer? activeLayer) {
        var layerElements = Collection.Children().OfType<Layer>();

        foreach (var element in layerElements) {
            if (element.Active) {
                activeLayer = element;
                return true;
            }
        }

        // no layers are active

        activeLayer = null;
        return false;
    }

    public bool CheckForAnyActive() {
        Debug.Log("Checking for any active layer");
        var result = TryGetActiveElement(out var layerElement);
        if (result) {
            Debug.Log("Checked and found an active layer");
            return true;
        }

        // no layers are active
        Debug.Log("Checked and found no active layers");
        return false;
    }

    /// <summary>
    ///     called when there is a layer set as active
    ///     this enables the New Effect Button visibility so that
    ///     a new effect can be created ONLY when a layer is selected
    /// </summary>
    public static event Action<bool> AnyLayersActive;

    public static event Action<EditorDiagram> OnLayerCreated;

    /// <summary>
    ///     this element was clicked to set this as the active element
    /// </summary>
    /// <param name="element"></param>
    void Select(Layer element) {
        var nothingSelected = !TryGetActiveElement(out var layerElement);
        if (nothingSelected) {
            Debug.Log("Select - no existing layers already selected");
            // no layer is currently selected, so this element is the new selection

            // when the selection changes to a different element than the previous selection
            element.SetActive();
            OnLayerSelectionChanged?.Invoke(element); // the active value in an element has been changed
            return;
        }

        // an element is already selected
        if (layerElement == element) {
            Debug.Log("Select - the selected layer is already active");

            // the element which is already active, as clicked
            // this deselects it, like a toggle
            element.SetInactive();
            OnLayerSelectionChanged?.Invoke(element); // the active value in an element has been changed
            return;
        }

        Debug.Log("Select - replace existing selected layer with new selection");
        // a different element was clicked than the already active elemnet
        // this changes the selection
        layerElement.SetInactive(); // old element is inactive
        element.SetActive(); // new element is active
        OnLayerSelectionChanged?.Invoke(element); // the active value in an element has been changed
    }
}
}