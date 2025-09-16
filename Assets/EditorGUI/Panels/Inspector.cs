using EditorGUI.Elements;
using EditorGUI.Panels.Managers;
using EditorGUI.Source.Effects.Base;
using EditorGUI.Source.Utility;
using UnityEngine.UIElements;

namespace EditorGUI.Panels {
[UxmlElement]
public partial class Inspector : VisualElement {
    readonly VisualElement _body;
    readonly VisualElement _header;
    readonly Label _headerText;
    readonly InspectorManager _inspectorManager;

    public Inspector() {
        AddToClassList("panel");

        _header = UIHelper.Create<VisualElement>("Header", "header");
        Add(_header);

        _body = UIHelper.Create<VisualElement>("Body", "body");
        Add(_body);

        _inspectorManager = new InspectorManager(_body);

        _headerText = UIHelper.Create<Label>("HeaderText", "header-text");
        _header.Add(_headerText);

        NodeElement.OnNodeSelectedEvent += OnNodeSelectionChanged;
        NodeElement.OnNoSelectedNodes += OnNoSelectedNodes;

        AssetHelper.LoadAssetPath<StyleSheet>("Assets/EditorGUI/Styles/Inspector.uss", OnStyleLoaded);

        // set default display name
        DisplayName = "Inspecting: None";
    }

    [UxmlAttribute]
    public string DisplayName {
        get => _headerText?.text ?? "";
        set => _headerText.text = value;
    }

    /// <summary>
    ///     a node has been selected
    /// </summary>
    /// <param name="node"></param>
    void OnNodeSelectionChanged(IEffect effect) {
        DisplayName = $"Inspecting: {effect.Name}";
    }

    /// <summary>
    ///     no diagrams are currently selected
    /// </summary>
    void OnNoSelectedNodes() {
        DisplayName = "Inspecting: None";
    }

    void OnStyleLoaded(StyleSheet uss) {
        if (uss != null) {
            styleSheets.Add(uss);
        }
    }
}
}