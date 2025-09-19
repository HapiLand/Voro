using EditorGUI.Source.Utility;
using UnityEngine.UIElements;

namespace EditorGUI.Panels {
[UxmlElement]
public partial class EditorColumn : VisualElement {
    readonly VisualElement _body;
    readonly VisualElement _footer;
    readonly VisualElement _header;
    readonly Label _headerText;

    public EditorColumn() {
        AddToClassList("panel");

        _header = UIHelper.Create<VisualElement>("Header", "header");
        Add(_header);

        _body = UIHelper.Create<VisualElement>("Body", "body");
        Add(_body);

        _footer = UIHelper.Create<VisualElement>("Footer", "footer");
        Add(_footer);

        _headerText = UIHelper.Create<Label>("HeaderText", "header-text");
        _header.Add(_headerText);

        AssetHelper.LoadAssetPath<StyleSheet>("Assets/EditorGUI/Styles/Inspector.uss", OnStyleLoaded);

        void OnStyleLoaded(StyleSheet uss) {
            if (uss != null) {
                styleSheets.Add(uss);
            }
        }
    }

    [UxmlAttribute]
    public string DisplayName {
        get => _headerText?.text ?? "";
        set => _headerText.text = value;
    }
}
}