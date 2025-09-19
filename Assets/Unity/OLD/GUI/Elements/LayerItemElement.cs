using System;
using EditorGUI.Source.Utility;
using UnityEngine.UIElements;

namespace OLD.GUI.Elements {
[UxmlElement]
public partial class LayerItemElement : VisualElement {
    readonly ButtonElement _selectBtn;
    readonly Label _text;

    public LayerItemElement() {
        AddToClassList("button");

        _text = new Label("Layer Item");
        _text.AddToClassList("text-button");

        _selectBtn = new ButtonElement { DisplayName = "Select" };
        _selectBtn.clicked += OnClicked; // hook up the actual event

        Add(_selectBtn);
        Add(_text);

        // callback for the entire element to be clickable
        // this.RegisterCallback<ClickEvent>(evt => OnClicked());

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