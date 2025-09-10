using UnityEngine.UIElements;
using VoroEditor.GUI.Elements;

namespace VoroEditor.GUI {
/// <summary>
///     add/remove/manage(select) layers
/// </summary>
public class LayerManager {
    readonly ScrollView _container;
    Layer _activeLayer;

    public LayerManager(VisualElement root) {
        _container = root.Q<ScrollView>("Layers");
        ToolbarManager.OnCreateLayer += CreateLayer;
    }

    void CreateLayer(string name) {
        var layer = new Layer();

        layer.BuildLayer(() => {
            // once the layer is built
            layer.DisplayName = name;
            _container.Add(layer);
            _activeLayer = layer; // ToDo selectable layer
        });
    }
}
}