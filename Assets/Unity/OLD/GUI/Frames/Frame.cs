using EditorGUI.Source.Utility;
using UnityEngine.UIElements;

namespace OLD.GUI.Frames {
[UxmlElement]
public partial class Frame : VisualElement {
    readonly Label _headerText;
    protected VisualElement _body;
    protected VisualElement _footer;
    protected VisualElement _header;

    bool _showFooter;

    public Frame() {
        // create elements
        _header = new VisualElement();
        _headerText = new Label();
        _body = new VisualElement();
        _footer = new VisualElement();

        // add to hierarchy
        Add(_header);
        Add(_body);
        ShowFooter = _showFooter;
        _header.Add(_headerText);

        // load styles
        AssetHelper.LoadAssetPath<StyleSheet>("Assets/GUI/Styles/Frame.uss", OnStyleLoaded);
        AssetHelper.LoadAssetPath<StyleSheet>("Assets/GUI/Styles/ColorStyle.uss", OnStyleLoaded);

        // set style
        AddToClassList("frame-container");
        AddToClassList("col-primary");

        _header.AddToClassList("frame-header");
        _header.AddToClassList("col-accent");

        _headerText.AddToClassList("frame-header-text");
        _headerText.AddToClassList("col-text-header");

        _body.AddToClassList("frame-body");
        _body.AddToClassList("col-primary");

        _footer.AddToClassList("frame-footer");
        _footer.AddToClassList("col-primary");

        return;

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

    [UxmlAttribute]
    public bool ShowFooter {
        get => _showFooter;
        set
        {
            if (_showFooter == value) {
                return;
            }

            _showFooter = value;

            if (_showFooter) {
                if (!_footer.parent?.Equals(this) ?? true) {
                    Add(_footer);
                }
            }
            else {
                if (_footer.parent == this) {
                    Remove(_footer);
                }
            }
        }
    }

    public void AddElementToHeader(VisualElement element) {
        _header.Add(element);
    }

    public void AddElementToBody(VisualElement element) {
        _body.Add(element);
    }

    public void AddElementToFooter(VisualElement element) {
        _footer.Add(element);
    }
}
}