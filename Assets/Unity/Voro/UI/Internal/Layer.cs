using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace Voro.UI.Internal {
class Layer : VisualElement {
    readonly Label _label;
    public bool Active;
    public List<Node> Nodes = new();

    public Layer() {
        style.paddingTop = new Length(10, LengthUnit.Pixel);
        style.paddingBottom = new Length(10, LengthUnit.Pixel);
        style.marginTop = new Length(5, LengthUnit.Pixel);
        style.marginBottom = new Length(5, LengthUnit.Pixel);

        _label = new Label();
        Add(_label);
        DisplayName = "Layer";

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

    public event Action<Layer> Clicked;
    public event Action<Layer> Selected;

    public override string ToString() {
        return $"{nameof(DisplayName)}: {DisplayName}, {nameof(Active)}: {Active}";
    }
}
}