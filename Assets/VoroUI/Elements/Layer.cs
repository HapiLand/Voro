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
        _label.style.color = Color.aquamarine;
        Active = true;
    }

    public void SetInactive() {
        _label.style.color = Color.softRed;
        Active = false;
    }

    void OnClicked() {
        Clicked?.Invoke();
    }
}
}