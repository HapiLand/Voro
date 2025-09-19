using EditorGUI.Panels.Managers;
using EditorGUI.Source.Utility;
using UnityEngine.UIElements;
using Button = EditorGUI.Elements.Internal.Button;

namespace EditorGUI.Panels {
/// <summary>
///     allows for the creation and managing of the diagrams that are part of this terrain generation
///     each diagram works as a layer, holding a set of effects
/// </summary>
[UxmlElement]
public partial class DiagramCollection : VisualElement {
    readonly VisualElement _body;
    readonly DiagramManager _diagramManager;
    readonly VisualElement _footer;
    readonly VisualElement _header;
    readonly Label _headerText;

    public DiagramCollection() {
        AddToClassList("panel");

        _header = UIHelper.Create<VisualElement>("Header", "header");
        _header.style.flexDirection = FlexDirection.Row;
        Add(_header);

        _body = UIHelper.Create<VisualElement>("Body", "body");
        Add(_body);

        _footer = UIHelper.Create<VisualElement>("Footer", "footer");
        Add(_footer);

        _diagramManager = new DiagramManager(_body);

        _headerText = UIHelper.Create<Label>("HeaderText", "header-text");
        _header.Add(_headerText);
        var newDiagramBtn = new Button { DisplayName = "New Diagram" };
        _footer.Add(newDiagramBtn);
        newDiagramBtn.clicked += () => {
            // call when the button is pressed
            var newDiagram = _diagramManager.CreateDiagramElement();
            _body.Add(newDiagram);
        };

        AssetHelper.LoadAssetPath<StyleSheet>("Assets/EditorGUI/Styles/Layers.uss", OnStyleLoaded);

        // set default display name
        DisplayName = "Generation Layers";
    }

    [UxmlAttribute]
    public string DisplayName {
        get => _headerText?.text ?? "";
        set => _headerText.text = value;
    }

    void OnStyleLoaded(StyleSheet uss) {
        if (uss != null) {
            styleSheets.Add(uss);
        }
    }
}
}