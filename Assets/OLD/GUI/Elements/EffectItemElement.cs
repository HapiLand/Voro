using EditorGUI.Source.Utility;
using UnityEngine;
using UnityEngine.UIElements;

namespace OLD.GUI.Elements {
[UxmlElement]
public partial class EffectItemElement : VisualElement {
    readonly Label _description;
    readonly ButtonElement _selectBtn;
    readonly ButtonElement _toggleBtn;
    VisualElement _body;
    VisualElement _header;
    Label _textHeader;

    public EffectItemElement() {
        AddToClassList("frame");

        AddHeader();
        AddBody();

        var stylePath = "Assets/GUI/Styles/GlobalStyle.uss";
        AssetHelper.LoadAssetPath<StyleSheet>(stylePath, OnStyleLoaded);

        _header.style.flexDirection = FlexDirection.Row; // horizontal row of buttons
        AddHeaderButton("Select");
        AddHeaderButton("Toggle");

        _description = new Label("--Description--");
        _description.AddToClassList("text-button");
        _body.Add(_description);
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

    void AddHeaderButton(string displayName) {
        var button = new ButtonElement { DisplayName = displayName };
        button.clicked += () => Debug.Log($"Effect.{displayName}");
        _header.Add(button);
    }
}
}