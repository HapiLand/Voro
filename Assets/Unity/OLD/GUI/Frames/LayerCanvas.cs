using OLD.GUI.Elements;
using UnityEngine.UIElements;

namespace OLD.GUI.Frames {
/// <summary>
///     displays the contents of the active layer
/// </summary>
[UxmlElement]
public partial class LayerCanvas : Frame {
    public LayerCanvas() {
        DisplayName = "Layer Canvas";
        ShowFooter = false;

        // add a vertical row of buttons
        _body.style.flexDirection = FlexDirection.Column;
        AddLayerItem("Slope");
        AddLayerItem("Noise");
        AddLayerItem("Terrace");
        return;

        void AddLayerItem(string displayName) {
            var button = new LayerItem { DisplayName = displayName };
            // button.clicked += () => Debug.Log($"Toolbar {button.DisplayName}");
            AddElementToBody(button);
        }
    }
}
}