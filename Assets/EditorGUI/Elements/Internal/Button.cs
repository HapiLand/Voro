using System;
using EditorGUI.Source.Utility;
using UnityEngine.UIElements;

namespace EditorGUI.Elements.Internal {
[UxmlElement]
public partial class Button : VisualElement {
    readonly Label _bodyText;

    public Button() {
        AddToClassList("panel");
        style.flexGrow = 0;

        _bodyText = UIHelper.Create<Label>("BodyText", "body-text");
        Add(_bodyText);

        AssetHelper.LoadAssetPath<StyleSheet>("Assets/EditorGUI/Styles/Button.uss", OnStyleLoaded);

        // handle events
        var clickable = new Clickable(OnClicked);
        this.AddManipulator(clickable);
    }

    [UxmlAttribute]
    public string DisplayName {
        get => _bodyText?.text ?? "";
        set => _bodyText.text = value;
    }

    void OnStyleLoaded(StyleSheet uss) {
        if (uss != null) {
            styleSheets.Add(uss);
        }
    }

    public event Action clicked;

    void OnClicked() {
        clicked?.Invoke();
    }
}
}