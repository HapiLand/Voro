using System;
using UnityEngine;
using UnityEngine.UIElements;
using VoroEditor.Source;

namespace VoroEditor.GUI.Elements {
[UxmlElement]
public partial class Layer : VisualElement {
    static Layer _selectedLayer;
    ScrollView _effects;
    Label _label;
    Toggle _toggle;
    public VisualElement Root;

    #region UXML Attributes

    [UxmlAttribute]
    public string DisplayName {
        get => _label?.text ?? "";
        set => _label.text = value;
    }

    #endregion

    public static event Action<Layer> OnLayerSelectedEvent;
    public static event Action OnLayerUnselectedEvent;

    public void BuildLayer(Action onLayerBuilt = null) {
        var path = "Assets/VoroEditor/GUI/Elements/Layer.uxml";
        AssetHelper.LoadAssetPath<VisualTreeAsset>(path, OnLoaded);
        return;

        void OnLoaded(VisualTreeAsset vta) {
            if (vta != null) {
                // instance the UXML
                var templateContainer = vta.Instantiate();
                templateContainer.style.flexGrow = 1; // required as default is set to 0
                templateContainer.name = "NewLayer";
                Root = templateContainer;

                Add(templateContainer);
                _effects = this.Q<ScrollView>("Effects");
                _label = this.Q<Label>("Label");
                _toggle = this.Q<Toggle>("Toggle");
                _toggle.RegisterValueChangedCallback(OnToggleValueChanged);
                onLayerBuilt?.Invoke(); // notify when the layer was built
            }
        }
    }

    void OnToggleValueChanged(ChangeEvent<bool> evt) {
        if (evt.newValue) {
            Select();
        }
        else if (_selectedLayer == this) {
            _selectedLayer = null;
            Root.RemoveFromClassList("layer-active");
            // unselection event so the inspector will no longer display this node
            OnLayerUnselectedEvent?.Invoke();
        }
    }

    void Select() {
        if (_selectedLayer != null && _selectedLayer != this) {
            _selectedLayer.Root.RemoveFromClassList("layer-active");
            var previousToggle = _selectedLayer.Q<Toggle>();
            if (previousToggle != null) {
                previousToggle.SetValueWithoutNotify(false);
            }
        }

        _selectedLayer = this;
        Root.AddToClassList("layer-active");

        Debug.Log($"layer {DisplayName} was selected");
        OnLayerSelectedEvent?.Invoke(this);
    }

    public void AddEffect(string effectInstance) {
        Debug.Log($"adding {effectInstance} to this layer");
    }
}
}