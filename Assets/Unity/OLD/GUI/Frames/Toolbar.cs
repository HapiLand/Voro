using UnityEngine;
using UnityEngine.UIElements;
using Button = OLD.GUI.Elements.Button;

namespace OLD.GUI.Frames {
[UxmlElement]
public partial class Toolbar : Frame {
    public Toolbar() {
        DisplayName = "Toolbar";
        ShowFooter = false;

        // add a horizontal row of buttons
        _body.style.flexDirection = FlexDirection.Row;
        AddToolbarButton("Save");
        AddToolbarButton("Load");
        AddToolbarButton("Refresh");
        AddToolbarButton("Settings");
        return;

        void AddToolbarButton(string displayName) {
            var button = new Button { DisplayName = displayName };
            button.clicked += () => Debug.Log($"Toolbar {button.DisplayName}");
            AddElementToBody(button);
        }
    }
}
}