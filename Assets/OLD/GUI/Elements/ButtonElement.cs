using System;
using EditorGUI.Source.Utility;
using UnityEngine.UIElements;

namespace OLD.GUI.Elements {
[UxmlElement]
public partial class ButtonElement : VisualElement {
    readonly Label _text;

    public ButtonElement() {
        AddToClassList("button");

        _text = new Label("Button");
        _text.AddToClassList("text-button");
        Add(_text);

        // handle clicking
        var clickable = new Clickable(OnClicked);
        this.AddManipulator(clickable);

        var stylePath = "Assets/GUI/Styles/GlobalStyle.uss";
        AssetHelper.LoadAssetPath<StyleSheet>(stylePath, OnStyleLoaded);
    }

    [UxmlAttribute]
    public string DisplayName {
        get => _text?.text ?? "";
        set => _text.text = value;
    }

    public event Action clicked;

    void OnStyleLoaded(StyleSheet uss) {
        if (uss != null) {
            styleSheets.Add(uss);
        }
    }

    void OnClicked() {
        clicked?.Invoke();
    }
}
}