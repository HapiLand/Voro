using EditorGUI.Source.Utility;
using UnityEngine.UIElements;

namespace OLD.GUI.Frames {
[UxmlElement]
public partial class TemplateFrame : VisualElement {
    protected VisualElement _body;
    protected VisualElement _header;
    Label _textHeader;

    public TemplateFrame() {
        AddToClassList("frame");

        AddHeader();
        AddBody();

        var stylePath = "Assets/GUI/Styles/GlobalStyle.uss";
        AssetHelper.LoadAssetPath<StyleSheet>(stylePath, OnStyleLoaded);
    }

    [UxmlAttribute]
    public string DisplayName {
        get => _textHeader?.text ?? "";
        set => _textHeader.text = value;
    }

    void AddBody() {
        _body = new VisualElement();
        _body.AddToClassList("frame-body");
        Add(_body);
    }

    void AddHeader() {
        _header = new VisualElement();
        _header.AddToClassList("frame-header");
        Add(_header);

        _textHeader = new Label("Heading Text");
        _textHeader.AddToClassList("text-header");
        _header.Add(_textHeader);
    }

    void OnStyleLoaded(StyleSheet uss) {
        if (uss != null) {
            styleSheets.Add(uss);
        }
    }
}
}