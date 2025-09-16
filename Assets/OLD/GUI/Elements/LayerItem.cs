using EditorGUI.Source.Utility;
using UnityEngine.UIElements;

namespace OLD.GUI.Elements {
[UxmlElement]
public partial class LayerItem : VisualElement {
    readonly Label _layerText;

    public LayerItem() {
        // create elements
        _layerText = new Label();

        // add to hierarchy
        Add(_layerText);

        // load styles
        AssetHelper.LoadAssetPath<StyleSheet>("Assets/GUI/Styles/Button.uss", OnStyleLoaded);
        AssetHelper.LoadAssetPath<StyleSheet>("Assets/GUI/Styles/ColorStyle.uss", OnStyleLoaded);

        // set style
        AddToClassList("button-body");
        AddToClassList("col-primary");

        _layerText.AddToClassList("button-text");
        _layerText.AddToClassList("col-text-body");

        // handle events
        // var clickable = new Clickable(OnClicked);
        // this.AddManipulator(clickable);
    }

    [UxmlAttribute]
    public string DisplayName {
        get => _layerText?.text ?? "";
        set => _layerText.text = value;
    }

    // public event Action clicked;

    void OnStyleLoaded(StyleSheet uss) {
        if (uss != null) {
            styleSheets.Add(uss);
        }
    }

    // void OnClicked() {
    //     clicked?.Invoke();
    // }
}
}