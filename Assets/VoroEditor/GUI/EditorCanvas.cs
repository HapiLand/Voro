using Source.Utility;
using UnityEngine.UIElements;

namespace VoroEditor.GUI {
/// <summary>
///     the core elements of GUI: layers,effects,etc
///     all share the same structure and logic that
///     shall allow for frictionless communication
/// </summary>
[UxmlElement]
public partial class EditorCanvas : VisualElement {
    public VisualElement Body;
    public VisualElement Footer;
    public VisualElement Header;

    public EditorCanvas() {
        AddToClassList("background");
        style.flexDirection = FlexDirection.Column;

        // create header
        Header = new VisualElement();
        Header.AddToClassList("header");
        Add(Header);

        // create body
        Body = new VisualElement();
        Body.AddToClassList("body");
        Add(Body);

        // create footer
        Footer = new VisualElement();
        Footer.AddToClassList("footer");
        Add(Footer);

        AssetUtil.LoadAssetPath<StyleSheet>("Assets/VoroEditor/GUI/StyleSheets/CanvasStyle.uss", OnStyleLoaded);

        void OnStyleLoaded(StyleSheet uss) {
            if (uss is null) {
                return;
            }

            styleSheets.Add(uss);
        }
    }
}
}