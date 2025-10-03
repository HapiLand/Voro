using System;
using UnityEngine;
using UnityEngine.UIElements;

namespace Voro.UI.Internal {
class Node : VisualElement {
    readonly Label _label;
    public bool Active;

    public Node() {
        style.paddingTop = new Length(10, LengthUnit.Pixel);
        style.paddingBottom = new Length(10, LengthUnit.Pixel);
        style.marginTop = new Length(5, LengthUnit.Pixel);
        style.marginBottom = new Length(5, LengthUnit.Pixel);

        _label = new Label();
        Add(_label);
        DisplayName = "Node";

        var clickable = new Clickable(OnClicked);
        this.AddManipulator(clickable);

        SetInactive();
    }

    [UxmlAttribute]
    public string DisplayName {
        get => _label?.text ?? "";
        set => _label.text = value;
    }

    void OnClicked() {
        Clicked?.Invoke(this);
    }

    public void SetActive() {
        Active = true;
        _label.style.color = Color.black;
        style.backgroundColor = Color.paleGreen;
        Selected?.Invoke(this);
    }

    public void SetInactive() {
        Active = false;
        _label.style.color = Color.red;
        style.backgroundColor = Color.aliceBlue;
    }

    public event Action<Node> Clicked;
    public event Action<Node> Selected;

    public override string ToString() {
        return $"{nameof(DisplayName)}: {DisplayName}, {nameof(Active)}: {Active}";
    }
}
}