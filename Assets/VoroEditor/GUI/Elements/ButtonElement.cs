using System;
using Source.Utility;
using UnityEngine.UIElements;

namespace VoroEditor.GUI.Elements {
[UxmlElement]
public partial class ButtonElement : VisualElement {
    readonly Label _label;

    public ButtonElement() {
        AddToClassList("background");

        _label = new Label
        {
            name = "Label"
        };
        _label.AddToClassList("text");
        Add(_label);

        AssetUtil.LoadAssetPath<StyleSheet>("Assets/VoroEditor/GUI/StyleSheets/ButtonStyle.uss", OnStyleLoaded);

        void OnStyleLoaded(StyleSheet uss) {
            if (uss is null) {
                return;
            }

            styleSheets.Add(uss);
        }

        var clickable = new Clickable(OnClicked);
        this.AddManipulator(clickable);
    }

    [UxmlAttribute]
    public string DisplayName {
        get => _label?.text ?? "";
        set => _label.text = value;
    }

    public event Action Clicked;

    void OnClicked() {
        Clicked?.Invoke();
    }
}
}