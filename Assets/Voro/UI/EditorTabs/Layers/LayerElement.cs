using System;
using UnityEngine;
using UnityEngine.UIElements;
using Voro.UI.Elements;

namespace Voro.UI.EditorTabs.Layers {
public class LayerElement : MenuEntry {
    bool _active;
    Label _label;

    public LayerElement(LayerInfo info) {
        CreateElements();
        SetStyle();
        DisplayName = info.Name;
        Active = info.Active;
        var clickable = new Clickable(OnClicked);
        this.AddManipulator(clickable);
        return;

        void SetStyle() {
            style.paddingTop = new Length(10, LengthUnit.Pixel);
            style.paddingBottom = new Length(10, LengthUnit.Pixel);
            style.marginTop = new Length(5, LengthUnit.Pixel);
            style.marginBottom = new Length(5, LengthUnit.Pixel);
            style.backgroundColor = Color.aliceBlue;
            _label.style.color = Color.black;
        }

        void CreateElements() {
            _label = new Label();
            Add(_label);
        }
    }

    [UxmlAttribute]
    public string DisplayName {
        get => _label?.text ?? "";
        set => _label.text = value;
    }

    [UxmlAttribute]
    public bool Active {
        get => _active;
        set
        {
            _active = value;
            if (_active) {
                SetActive();
            }
            else {
                SetInactive();
            }
        }
    }

    public event Action<bool> Clicked;

    void OnClicked() {
        Active = !Active;
        Clicked?.Invoke(Active);
    }

    void SetActive() {
        style.backgroundColor = Color.paleGreen;
    }

    void SetInactive() {
        style.backgroundColor = Color.salmon;
    }
}
}