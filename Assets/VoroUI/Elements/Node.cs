using System;
using UnityEngine;
using UnityEngine.UIElements;
using VoroUI.Effects;

namespace VoroUI.Elements {
public class Node : VisualElement {
    /// <summary>
    ///     element to contain the field controls
    /// </summary>
    readonly VisualElement _controlContainer;

    readonly Label _label;

    /// <summary>
    ///     this is the current selection
    /// </summary>
    public bool Active;

    public IEffect Effect;
    // todo display FieldControlElements from the effect

    // todo move element in collection
    // todo remove element from collection
    public Node(IEffect effect) {
        Effect = effect;
        // label
        _label = new Label();
        DisplayName = effect.Name;
        Add(_label);
        // container to store the IEffect controls
        _controlContainer = new VisualElement();
        Add(_controlContainer);
        // make element selectable
        var clickable = new Clickable(OnClicked);
        this.AddManipulator(clickable);
        SetInactive();
    }

    [UxmlAttribute]
    public string DisplayName {
        get => _label?.text ?? "";
        set => _label.text = value;
    }

    public event Action Clicked;

    public void SetActive() {
        _label.style.color = Color.aquamarine;
        Active = true;

        // display the field controls within IEffect
        foreach (var control in Effect.Controls) {
            _controlContainer.Add(control);
        }
    }

    public void SetInactive() {
        _label.style.color = Color.softRed;
        Active = false;

        _controlContainer.Clear(); // remove any controls as the element isnt displaying them anymore
    }

    void OnClicked() {
        Clicked?.Invoke();
    }
}
}