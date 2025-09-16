using EditorGUI.Elements;
using EditorGUI.Panels.Managers;
using EditorGUI.Source.Utility;
using UnityEngine.UIElements;
using Button = EditorGUI.Elements.Internal.Button;

namespace EditorGUI.Panels {
[UxmlElement]
public partial class NodeCollection : VisualElement {
    readonly VisualElement _body;
    readonly VisualElement _footer;
    readonly VisualElement _header;
    readonly Label _headerText;
    readonly NodeManager _nodeManager;

    public NodeCollection() {
        AddToClassList("panel");

        _header = UIHelper.Create<VisualElement>("Header", "header");
        _header.style.flexDirection = FlexDirection.Row;
        Add(_header);

        _body = UIHelper.Create<VisualElement>("Body", "body");
        Add(_body);

        _footer = UIHelper.Create<VisualElement>("Footer", "footer");
        Add(_footer);

        _nodeManager = new NodeManager(_body);

        _headerText = UIHelper.Create<Label>("HeaderText", "header-text");
        _header.Add(_headerText);
        var newNodeBtn = new Button { DisplayName = "New Node" };
        _footer.Add(newNodeBtn);
        newNodeBtn.clicked += () => { _nodeManager.AddNewNode(); };

        DiagramElement.OnDiagramSelectedEvent += OnDiagramSelectionChanged;
        DiagramElement.OnNoSelectedDiagrams += OnNoSelectedDiagrams;

        AssetHelper.LoadAssetPath<StyleSheet>("Assets/EditorGUI/Styles/Nodes.uss", OnStyleLoaded);

        // set default display name
        DisplayName = "Selected Layer: None";
    }

    [UxmlAttribute]
    public string DisplayName {
        get => _headerText?.text ?? "";
        set => _headerText.text = value;
    }

    /// <summary>
    ///     a diagram has been selected
    /// </summary>
    /// <param name="element"></param>
    void OnDiagramSelectionChanged(DiagramElement element) {
        DisplayName = $"Selected Layer: {element.DisplayName}";
    }

    /// <summary>
    ///     no diagrams are currently selected
    /// </summary>
    void OnNoSelectedDiagrams() {
        DisplayName = "Selected Layer: None";
    }

    void OnStyleLoaded(StyleSheet uss) {
        if (uss != null) {
            styleSheets.Add(uss);
        }
    }
}
}