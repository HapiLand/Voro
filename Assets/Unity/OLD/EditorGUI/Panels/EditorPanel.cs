using System;
using System.Collections.Generic;
using System.Linq;
using EditorGUI.Elements;
using EditorGUI.Source.Effects;
using EditorGUI.Source.Effects.Base;
using EditorGUI.Source.Utility;
using UnityEngine;
using UnityEngine.UIElements;
using Button = EditorGUI.Elements.Internal.Button;

namespace EditorGUI.Panels {
[UxmlElement]
public partial class EditorPanel : VisualElement {
    readonly VisualElement _body;
    readonly VisualElement _controlContainer;
    readonly VisualElement _effectContainer;
    readonly VisualElement _layerContainer;

    public Dictionary<LayerElement, List<EffectElement>> EditorLayers = new();
    // todo removing the elements with the X button doesnt clear it from the dictionary

    public EditorPanel() {
        AddToClassList("panel");

        style.flexDirection = FlexDirection.Row;
        style.flexGrow = 1;

        #region Layers

        var layerColumn = new EditorColumn { name = "Layers", DisplayName = "Layers" };
        Add(layerColumn);
        _layerContainer = layerColumn.Q<VisualElement>("Body");
        var layerFooter = layerColumn.Q<VisualElement>("Footer");
        var newLayerButton = new Button { DisplayName = "New Layer" };
        layerFooter.Add(newLayerButton);
        newLayerButton.clicked += () => {
            var newLayer = CreateLayerElement();
            _layerContainer.Add(newLayer);
        };

        #endregion

        #region Effects

        var effectColumn = new EditorColumn { name = "Effects", DisplayName = "Effects" };
        Add(effectColumn);
        _effectContainer = effectColumn.Q<VisualElement>("Body");
        var effectFooter = effectColumn.Q<VisualElement>("Footer");
        var newEffectButton = new Button { DisplayName = "New Effect" };
        effectFooter.Add(newEffectButton);
        newEffectButton.clicked += () => {
            var activeLayer = LayerElement.ActiveLayer;
            if (activeLayer == null) {
                Debug.LogWarning("no layers active, cannot create new effect");
                return;
            }

            var newEffect = CreateEffectElement();
            _effectContainer.Add(newEffect); // store effect in the container
            if (EditorLayers.TryGetValue(activeLayer, out var effects)) {
                effects.Add(newEffect);
            }

            OnEffectCreated?.Invoke(newEffect.name);
        };

        #endregion

        #region Controls

        var controlColumn = new EditorColumn { name = "Controls", DisplayName = "Controls" };
        Add(controlColumn);
        _controlContainer = controlColumn.Q<VisualElement>("Body");

        #endregion

        AssetHelper.LoadAssetPath<StyleSheet>("Assets/EditorGUI/Styles/Inspector.uss", OnStyleLoaded);
        return;


        void OnStyleLoaded(StyleSheet uss) {
            if (uss != null) {
                styleSheets.Add(uss);
            }
        }
    }

    public event Action<string> OnLayerCreated;
    public event Action<string> OnEffectCreated;

    LayerElement CreateLayerElement(Action<LayerElement> onClicked = null) {
        var layerName = EditorLayers.Count() switch
        {
            0 => "Player Spawn",
            1 => "Flat",
            2 => "Grass",
            3 => "Cliffs",
            4 => "Safehouse",
            5 => "Level Border",
            6 => "Forest",
            7 => "Props",
            8 => "Paths",
            9 => "Water",
            _ => "Null"
        };
        var newLayer = new LayerElement { name = $"Layer_{layerName}", DisplayName = layerName };
        // add new layer to the dictionary entry
        EditorLayers[newLayer] = new List<EffectElement>();

        newLayer.clicked += () => {
            OnLayerCreated?.Invoke(layerName);
            onClicked?.Invoke(newLayer);

            _effectContainer.Clear(); // clear any effects on display
            _controlContainer.Clear(); // clear any controls on display
            Debug.Log("effect container cleared");

            // the layer is selected, display the effects it contains
            if (EditorLayers.TryGetValue(newLayer, out var effects)) {
                foreach (var effect in effects) {
                    _effectContainer.Add(effect);
                }
            }
        };
        return newLayer;
    }

    EffectElement CreateEffectElement(Action<EffectElement> onClicked = null) {
        var newEffect = new EffectElement();
        var effectName = newEffect.EffectInstance.Name; // read the IEffect name

        newEffect.clicked += () => {
            Debug.Log($"{effectName} Selected");
            OnEffectCreated?.Invoke(effectName); // notify any external listeners
            onClicked?.Invoke(newEffect); // call back to the editor panel

            _controlContainer.Clear(); // clear any controls on display

            // the effect has been selected, display the inspector controls
            CreateControlElement(newEffect.EffectInstance);
        };
        return newEffect;
    }

    void CreateControlElement(IEffect effect) {
        switch (effect) {
        // add the controls from the node to the inspector
        case SlopeEffect slope:
            _controlContainer.Add(slope.InspectorControls);
            break;
        }
    }
}
}