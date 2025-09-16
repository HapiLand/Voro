using OLD.GUI.Elements;
using UnityEngine;
using UnityEngine.UIElements;

namespace OLD.GUI.Frames {
[UxmlElement]
public partial class ToolbarFrame : TemplateFrame {
    public ToolbarFrame() {
        DisplayName = "Toolbar";

        _body.style.flexDirection = FlexDirection.Row; // horizontal row of buttons

        foreach (var s in new[]
                 {
                     "Save",
                     "Load",
                     "Refresh",
                     "Settings"
                 }) {
            AddToolbarButton(s);
        }
    }

    void AddToolbarButton(string displayName) {
        var button = new ButtonElement { DisplayName = displayName };
        button.clicked += () => Debug.Log($"Toolbar.{displayName}");
        _body.Add(button);
    }
}
}