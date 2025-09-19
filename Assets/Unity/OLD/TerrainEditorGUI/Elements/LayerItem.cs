using EditorGUI.Source.Utility;
using UnityEngine.UIElements;

namespace OLD.TerrainEditorGUI.Elements {
[UxmlElement]
public partial class LayerItem : VisualElement {
    readonly Label _label;

    public LayerItem() {
        AddToClassList("layerItemElement");

        _label = new Label("Layer Name");
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