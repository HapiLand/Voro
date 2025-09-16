using System;
using EditorGUI.Source.Utility;
using UnityEngine.UIElements;

namespace OLD.GUI.Elements {
[UxmlElement]
public partial class Button : VisualElement {
    readonly Label _buttonText;

    public Button() {
        // create elements
        _buttonText = new Label();

        // add to hierarchy
        Add(_buttonText);

        // load styles
        AssetHelper.LoadAssetPath<StyleSheet>("Assets/GUI/Styles/Button.uss", OnStyleLoaded);
        AssetHelper.LoadAssetPath<StyleSheet>("Assets/GUI/Styles/ColorStyle.uss", OnStyleLoaded);

        // set style
        AddToClassList("button-body");
        AddToClassList("col-primary");

        _buttonText.AddToClassList("button-text");
        _buttonText.AddToClassList("col-text-body");

        // handle events
        var clickable = new Clickable(OnClicked);
        this.AddManipulator(clickable);
    }

    [UxmlAttribute]
    public string DisplayName {
        get => _buttonText?.text ?? "";
        set => _buttonText.text = value;
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