using EditorGUI.Source.Utility;
using UnityEngine.UIElements;

namespace OLD.TerrainEditorGUI.Elements {
[UxmlElement]
public partial class Button : VisualElement {
    readonly Label _label;

    public Button() {
        AddToClassList("buttonElement");

        _label = new Label("Text");
        _label.name = "ButtonText";
        _label.AddToClassList("buttonText");
        Add(_label);

        var stylePath = "Assets/TerrainEditorGUI/GlobalStyle.uss";
        AssetHelper.LoadAssetPath<StyleSheet>(stylePath, OnLoaded);
    }

    [UxmlAttribute]
    public string DisplayName {
        get => _label?.text ?? "";
        set => _label.text = value;
    }

    void OnLoaded(StyleSheet uss) {
        if (uss != null) {
            styleSheets.Add(uss);
        }
    }
}
}