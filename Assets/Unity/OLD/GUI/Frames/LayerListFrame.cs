using OLD.GUI.Elements;
using OLD.GUI.Managers;
using UnityEngine.UIElements;

namespace OLD.GUI.Frames {
[UxmlElement]
public partial class LayerListFrame : TemplateFrame {
    readonly LayerManager _layerManager;

    public LayerListFrame() {
        DisplayName = "Layer List";
        _layerManager = new LayerManager();
        _header.style.flexDirection = FlexDirection.Row; // horizontal row of buttons

        var newLayerBtn = new ButtonElement { DisplayName = "New Layer" };
        newLayerBtn.clicked += () => {
            var layerButton = _layerManager.CreateLayerItem();
            _body.Add(layerButton);
        };
        _header.Add(newLayerBtn);

        // default layer
        var initialLayer = _layerManager.CreateLayerItem();
        _body.Add(initialLayer);
    }
}
}