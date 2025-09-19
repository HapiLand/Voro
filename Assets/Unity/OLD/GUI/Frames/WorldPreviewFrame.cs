using UnityEngine.UIElements;

namespace OLD.GUI.Frames {
[UxmlElement]
public partial class WorldPreviewFrame : TemplateFrame {
    VisualElement _footer;
    Label _textFooter;

    public WorldPreviewFrame() {
        DisplayName = "World Preview";
        AddFooter();
    }

    void AddFooter() {
        _footer = new VisualElement();
        _footer.AddToClassList("frame-footer");
        Add(_footer);

        _textFooter = new Label("Footer Text");
        _textFooter.AddToClassList("text-footer");
        _footer.Add(_textFooter);
    }
}
}