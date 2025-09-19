using System;
using System.Collections.Generic;
using EditorGUI.Source.Utility;
using UnityEngine;
using UnityEngine.UIElements;
using Button = EditorGUI.Elements.Internal.Button;

namespace EditorGUI.Elements {
/// <summary>
///     this is a user generated element that defines a specific form of terrain generation
///     the element contains a collection of Node elements, each is a different Effect
///     the diagram element will be used to produce a Computed VoroDiagram
/// </summary>
[UxmlElement]
public partial class DiagramElement : VisualElement {
    public static DiagramElement SelectedDiagram;
    readonly Label _bodyText;

    public bool Active;

    /// <summary>
    ///     the NodeElements that exist within this DiagramElement
    /// </summary>
    public List<NodeElement> NodeInstances = new(); // todo replace with actual node elements

    public DiagramElement() {
        AddToClassList("panel");
        style.flexGrow = 0;
        style.flexDirection = FlexDirection.Row;

        _bodyText = UIHelper.Create<Label>("BodyText", "body-text");

        var selectButton = new Button { DisplayName = "Select" };
        selectButton.clicked += OnSelectedButtonClicked;

        var deleteBtn = new Button { DisplayName = "✖" };
        var upBtn = new Button { DisplayName = "↑" };
        var downBtn = new Button { DisplayName = "↓" };

        deleteBtn.clicked += RemoveFromHierarchy;
        upBtn.clicked += () => { Move(-1); };
        downBtn.clicked += () => { Move(1); };

        Add(_bodyText);
        Add(selectButton);
        Add(deleteBtn);
        Add(upBtn);
        Add(downBtn);

        // placeholder nodes
        NodeInstances.Add(new NodeElement { DisplayName = "Slope", name = "SlopeEffect" });

        AssetHelper.LoadAssetPath<StyleSheet>("Assets/EditorGUI/Styles/DiagramElement.uss", OnStyleLoaded);
    }

    [UxmlAttribute]
    public string DisplayName {
        get => _bodyText?.text ?? "";
        set => _bodyText.text = value;
    }

    public static event Action<DiagramElement> OnDiagramSelectedEvent;
    public static event Action OnDiagramUnselectedEvent;
    public static event Action OnNoSelectedDiagrams;

    void OnSelectedButtonClicked() {
        Active = !Active;

        if (Active) {
            Select();
        }
        else if (SelectedDiagram == this) {
            Active = false;
            SelectedDiagram = null;
            RemoveFromClassList("panel-selected");
            OnDiagramUnselectedEvent?.Invoke();
        }

        if (SelectedDiagram == null) {
            OnNoSelectedDiagrams?.Invoke();
        }
    }

    /// <summary>
    ///     changes the position of the node in a up/down direction
    /// </summary>
    void Move(int direction) {
        var parent = this.parent;
        if (parent == null) {
            return;
        }

        // find the current index and the new index that is in the desired direction
        var index = parent.IndexOf(this);
        var newIndex = Mathf.Clamp(index + direction, 0, parent.childCount - 1);
        if (index == newIndex) {
            return;
        }

        // update order of nodes
        parent.Remove(this);
        parent.Insert(newIndex, this);
    }

    void Select() {
        if (SelectedDiagram != null && SelectedDiagram != this) {
            SelectedDiagram.Active = false;
            SelectedDiagram.RemoveFromClassList("panel-selected");
            var previousToggle = SelectedDiagram.Q<Toggle>(); // todo replace Toggle with the selection button
            if (previousToggle != null) {
                previousToggle.SetValueWithoutNotify(false);
            }
        }

        SelectedDiagram = this;
        AddToClassList("panel-selected");

        Debug.Log($"DiagramElement {SelectedDiagram.DisplayName} was selected");

        // notify inspector that it shall display this effect
        OnDiagramSelectedEvent?.Invoke(this);
    }

    void OnStyleLoaded(StyleSheet uss) {
        if (uss != null) {
            styleSheets.Add(uss);
        }
    }

    public event Action clicked;

    void OnClicked() {
        clicked?.Invoke();
    }
}
}