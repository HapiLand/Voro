using System;
using UnityEngine;
using UnityEngine.UIElements;
using Voro.UI.Elements;

namespace Voro.UI.EditorTabs.Nodes {
public class NodeElement : MenuEntry {
    readonly NodeInfo _info;
    bool _active;

    /// <summary>
    ///     ControlElements are stored in this element
    /// </summary>
    VisualElement _controls;

    Label _label;

    public NodeElement(NodeInfo info) {
        _info = info;

        CreateElements();
        SetStyle();

        DisplayName = info.Name.ToString();
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

            // create element that will display the controls
            _controls = new VisualElement();
            Add(_controls);
            // set initial visibility of the controls
            _controls.style.display = Active ? DisplayStyle.Flex : DisplayStyle.None;

            // add the controls to the element
            foreach (var control in _info.DataControl.Controls) {
                _controls.Add(control);
            }
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

            // display the controls for the element only when active=true
            _controls.style.display = _active ? DisplayStyle.Flex : DisplayStyle.None;

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