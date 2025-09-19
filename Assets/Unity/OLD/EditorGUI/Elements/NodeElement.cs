using System;
using EditorGUI.Source.Effects.Base;
using EditorGUI.Source.Utility;
using UnityEngine;
using UnityEngine.UIElements;
using Button = EditorGUI.Elements.Internal.Button;

namespace EditorGUI.Elements {
/// <summary>
///     this is a user chosen element that does a particular instruction for a DiagramElements generation
///     the element contains an Effect object which is what computes the effect
/// </summary>
[UxmlElement]
public partial class NodeElement : VisualElement {
    public static NodeElement SelectedNode;
    readonly Label _bodyText;
    public bool Active;

    /// <summary>
    ///     the IEffect that exists within this NodeElement
    /// </summary>
    public IEffect EffectInstance;

    public NodeElement() {
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

        // placeholder effects
        EffectInstance = EffectHelper.Create("Slope");

        AssetHelper.LoadAssetPath<StyleSheet>("Assets/EditorGUI/Styles/Node.uss", OnStyleLoaded);

        // handle events
        var clickable = new Clickable(OnClicked);
        this.AddManipulator(clickable);
    }

    [UxmlAttribute]
    public string DisplayName {
        get => _bodyText?.text ?? "";
        set => _bodyText.text = value;
    }

    public static event Action<IEffect> OnNodeSelectedEvent;
    public static event Action OnNodeUnselectedEvent;
    public static event Action OnNoSelectedNodes;

    void OnSelectedButtonClicked() {
        Active = !Active;

        if (Active) {
            Select();
        }
        else if (SelectedNode == this) {
            Active = false;
            SelectedNode = null;
            RemoveFromClassList("panel-selected");
            OnNodeUnselectedEvent?.Invoke();
        }

        if (SelectedNode == null) {
            OnNoSelectedNodes?.Invoke();
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
        if (SelectedNode != null && SelectedNode != this) {
            SelectedNode.Active = false;
            SelectedNode.RemoveFromClassList("panel-selected");
            var previousToggle = SelectedNode.Q<Toggle>(); // todo replace Toggle with the selection button
            if (previousToggle != null) {
                previousToggle.SetValueWithoutNotify(false);
            }
        }

        SelectedNode = this;
        AddToClassList("panel-selected");

        Debug.Log($"NodeElement {SelectedNode.DisplayName} was selected");

        // notify inspector that it shall display this effect
        OnNodeSelectedEvent?.Invoke(EffectInstance);
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