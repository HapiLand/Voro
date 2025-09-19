using System;
using EditorGUI.Elements;
using EditorGUI.Source.Utility;
using EditorGUI.Source.Voro;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.UIElements;
using Button = EditorGUI.Elements.Internal.Button;

namespace EditorGUI.Panels {
[UxmlElement]
public partial class Toolbar : VisualElement {
    readonly VisualElement _body;
    readonly VisualElement _header;
    readonly Label _headerText;

    public Toolbar() {
        AddToClassList("panel");
        style.flexGrow = 0;

        _header = UIHelper.Create<VisualElement>("Header", "header");
        Add(_header);

        _body = UIHelper.Create<VisualElement>("Body", "body");
        Add(_body);

        _headerText = UIHelper.Create<Label>("HeaderText", "header-text");
        _header.Add(_headerText);

        // add a horizontal row of buttons
        _body.style.flexDirection = FlexDirection.Row;

        var refreshButton = new Button { DisplayName = "Reload Scene" };
        refreshButton.clicked += Refresh;
        _body.Add(refreshButton);

        var manager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameWorldManager>();
        Debug.Log($"GameManager instance found by my GUI code: {manager}");
        
        // inside constructor
        var computeButton = new Button { DisplayName = "Compute World" };
        computeButton.clicked += () => manager.ExecuteComputeWorld();
        _body.Add(computeButton);

        AddToolbarButton("Save");
        AddToolbarButton("Load");
        AddToolbarButton("Settings");

        AssetHelper.LoadAssetPath<StyleSheet>("Assets/EditorGUI/Styles/Toolbar.uss", OnStyleLoaded);
        
        DisplayName = "Toolbar";

        return;

        void AddToolbarButton(string displayName) {
            var button = new Button();
            button.DisplayName = displayName;
            _body.Add(button);
        }
    }

    [UxmlAttribute]
    public string DisplayName {
        get => _headerText?.text ?? "";
        set => _headerText.text = value;
    }

    public static event Action<DiagramElement> OnCompute;

    void Refresh() {
        Debug.Log("WorldManager.Refresh - reload the scene");

        EditorSceneManager.OpenScene("Assets/Scenes/GameWorld.unity", OpenSceneMode.Single);
    }

    void Compute() {
        Debug.Log("WorldManager.Compute - process all diagrams");

        if (DiagramElement.SelectedDiagram == null) {
            Debug.LogError("Cannot compute when there are no diagrams");
            return;
        }

        OnCompute?.Invoke(DiagramElement.SelectedDiagram);
        // OnNodeSelectedEvent?.Invoke(EffectInstance);
    }

    void OnStyleLoaded(StyleSheet uss) {
        if (uss != null) {
            styleSheets.Add(uss);
        }
    }
}
}