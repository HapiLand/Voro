using EditorGUI.Source.Utility;
using UnityEngine.UIElements;

namespace EditorGUI.Panels {
[UxmlElement]
public partial class Preview : VisualElement {
    readonly VisualElement _body;
    readonly VisualElement _footer;
    readonly Label _footerText;
    readonly VisualElement _header;
    readonly Label _headerText;

    public Preview() {
        AddToClassList("panel");

        _header = UIHelper.Create<VisualElement>("Header", "header");
        Add(_header);

        _body = UIHelper.Create<VisualElement>("Body", "body");
        Add(_body);

        var draw = UIHelper.Create<VisualElement>("Draw", "draw");
        _body.Add(draw);

        _footer = UIHelper.Create<VisualElement>("Footer", "footer");
        Add(_footer);

        _headerText = UIHelper.Create<Label>("HeaderText", "header-text");
        _header.Add(_headerText);

        _footerText = UIHelper.Create<Label>("FooterText", "footer-text");
        _footerText.text = "Updating...";
        _footer.Add(_footerText);

        AssetHelper.LoadAssetPath<StyleSheet>("Assets/EditorGUI/Styles/Preview.uss", OnStyleLoaded);
    }

    [UxmlAttribute]
    public string DisplayName {
        get => _headerText?.text ?? "";
        set => _headerText.text = value;
    }

    void OnStyleLoaded(StyleSheet uss) {
        if (uss != null) {
            styleSheets.Add(uss);
        }
    }
}
}