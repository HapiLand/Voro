using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using VoroEditor.GUI.Elements;
using VoroEditor.Voro;

namespace VoroEditor.GUI.Panels {
/// <summary>
///     displays all Layers within the Editor as a list of elements
/// </summary>
[UxmlElement]
public partial class Layers : EditorCanvas {
    readonly List<Layer> _layerCollection = new();

    // todo display these layers as LayerElements
    public Layers() {
        Layer.LayerCreatedEvent += OnLayerCreatedEvent;

        var newLayerBtn = new ButtonElement { DisplayName = "New Layer" };
        Footer.Add(newLayerBtn);
        newLayerBtn.Clicked += () => { 
            Debug.Log("Clicked [NewLayer]");
            CreateNewLayer(out var newLayer);
        };
    }

    /// <summary>
    ///     Creates a new Layer for the collection
    /// </summary>
    void CreateNewLayer(out Layer newLayer) {
        Debug.Log("Creating new Layer");
        newLayer = new Layer("FooLayer");

        // store the layer in the collection
        _layerCollection.Add(newLayer);
        
        // refresh the canvas to display the LayerElements
        Body.Clear(); // reset the body contents
        foreach (var layer in _layerCollection) {
            Body.Add(layer.GetElement());
        }
    }

    void OnLayerCreatedEvent(Layer obj) {
        Debug.Log($"Layer {obj.Name} created");
    }
}
}