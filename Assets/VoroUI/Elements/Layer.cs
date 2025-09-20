using System;
using UnityEngine;
using UnityEngine.UIElements;

namespace VoroUI.Elements {
public class Layer : VisualElement {
    readonly Label _label;

    /// <summary>
    ///     this is the current selection
    /// </summary>
    public bool Active;

    public EditorDiagram EditorDiagram;

    // todo move element in collection
    // todo remove element from collection
    public Layer(EditorDiagram editorDiagram) {
        style.paddingTop = new Length(10, LengthUnit.Pixel); // element top size increase
        style.paddingBottom = new Length(10, LengthUnit.Pixel); // element bottom size increase
        style.marginTop = new Length(5, LengthUnit.Pixel); // top gap
        style.marginBottom = new Length(5, LengthUnit.Pixel); // bottom gap
        style.backgroundColor = Color.aliceBlue;

        EditorDiagram = editorDiagram;
        // label
        _label = new Label();
        DisplayName = EditorDiagram.Name;
        Add(_label);
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
        _label.style.color = Color.black;
        style.backgroundColor = Color.paleGreen;
        Active = true;
    }

    public void SetInactive() {
        _label.style.color = Color.red;
        style.backgroundColor = Color.aliceBlue;
        Active = false;
    }

    void OnClicked() {
        Clicked?.Invoke();
    }
}
}