using System;
using EditorGUI.Source.Effects.Base;
using EditorGUI.Source.Utility;
using UnityEngine;
using UnityEngine.UIElements;
using Button = EditorGUI.Elements.Internal.Button;

namespace EditorGUI.Elements {
[UxmlElement]
public partial class EffectElement : VisualElement {
    public static EffectElement ActiveEffect;
    readonly Label _bodyText;
    public bool Active;

    /// <summary>
    ///     the IEffect that exists within this EffectElement
    /// </summary>
    public IEffect EffectInstance;

    public EffectElement() {
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
        DisplayName = EffectInstance.Name;
        name = $"Effect_{DisplayName}";

        AssetHelper.LoadAssetPath<StyleSheet>("Assets/EditorGUI/Styles/DiagramElement.uss", OnStyleLoaded);

        void OnStyleLoaded(StyleSheet uss) {
            if (uss != null) {
                styleSheets.Add(uss);
            }
        }
    }

    [UxmlAttribute]
    public string DisplayName {
        get => _bodyText?.text ?? "";
        set => _bodyText.text = value;
    }

    public static event Action<EffectElement> OnDiagramSelectedEvent;
    public static event Action OnDiagramUnselectedEvent;
    public static event Action OnNoSelectedDiagrams;

    void OnSelectedButtonClicked() {
        Active = !Active;

        if (Active) {
            Select();
        }
        else if (ActiveEffect == this) {
            Active = false;
            ActiveEffect = null;
            RemoveFromClassList("panel-selected");
            OnDiagramUnselectedEvent?.Invoke();
        }

        if (ActiveEffect == null) {
            OnNoSelectedDiagrams?.Invoke();
        }
    }

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
        if (ActiveEffect != null && ActiveEffect != this) {
            ActiveEffect.Active = false;
            ActiveEffect.RemoveFromClassList("panel-selected");
            var previousToggle = ActiveEffect.Q<Toggle>(); // todo replace Toggle with the selection button
            if (previousToggle != null) {
                previousToggle.SetValueWithoutNotify(false);
            }
        }

        ActiveEffect = this;
        AddToClassList("panel-selected");

        Debug.Log($"EffectElement {ActiveEffect.DisplayName} was selected");

        // notify inspector that it shall display this effect
        OnDiagramSelectedEvent?.Invoke(this);

        OnClicked();
    }

    public event Action clicked;

    void OnClicked() {
        clicked?.Invoke();
    }
}
}