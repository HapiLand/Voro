using System;
using System.Collections.Generic;
using EditorGUI.Elements;

namespace VoroEditor.Voro {
/// <summary>
///     Layer is a user-generated object to produce a unique form of terrain generation
///     stores a collection of effects added by the user
/// </summary>
public class Layer {
    List<Effect> _effects = new();
    public string Name;

    public Layer(string layerName) {
        Name = layerName;
        LayerCreatedEvent?.Invoke(this);
    }

    public static event Action<Layer> LayerCreatedEvent;

    public LayerElement GetElement() {
        return new LayerElement { DisplayName = Name };
        // todo return LayerElement
    }
}
}