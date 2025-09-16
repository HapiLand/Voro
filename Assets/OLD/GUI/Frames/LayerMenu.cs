using OLD.GUI.Elements;
using UnityEngine.UIElements;

namespace OLD.GUI.Frames {
/// <summary>
///     displays the list of layers currently in use
/// </summary>
[UxmlElement]
public partial class LayerMenu : Frame {
    public LayerMenu() {
        DisplayName = "Layer Menu";
        ShowFooter = false;

        // add a vertical row of buttons
        _body.style.flexDirection = FlexDirection.Column;
        AddLayerItem("Lorem");
        AddLayerItem("Ipsum");
        return;

        void AddLayerItem(string displayName) {
            var button = new LayerItem { DisplayName = displayName };
            // button.clicked += () => Debug.Log($"Toolbar {button.DisplayName}");
            AddElementToBody(button);
        }
    }
}
}