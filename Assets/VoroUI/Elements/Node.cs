using System;
using UnityEngine;
using UnityEngine.UIElements;
using VoroWorld.Generation.Effects.Base;
using VoroWorld.Generation.Effects.Internal;

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

    public EffectNames Effect;
    // todo display FieldControlElements from the effect

    // todo move element in collection
    // todo remove element from collection
    public Node(EffectNames effect) {
        style.paddingTop = new Length(10, LengthUnit.Pixel); // element top size increase
        style.paddingBottom = new Length(10, LengthUnit.Pixel); // element bottom size increase
        style.marginTop = new Length(5, LengthUnit.Pixel); // top gap
        style.marginBottom = new Length(5, LengthUnit.Pixel); // bottom gap
        style.backgroundColor = Color.aliceBlue;

        Effect = effect;
        // label
        _label = new Label();
        DisplayName = effect.ToString();
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
        _label.style.color = Color.black;
        style.backgroundColor = Color.paleGreen;
        Active = true;

        // display the field controls within IEffect
        // todo derive class from Node to store the control configurations
        //  this is in order to seperate this from IEffect
        //  the Node Element will only store data and a name of the effect
        //  the actual IEffect that enum points to is handled by the compute class
        foreach (var control in Effect.Controls) {
            _controlContainer.Add(control);
        }
    }

    public void SetInactive() {
        _label.style.color = Color.red;
        style.backgroundColor = Color.aliceBlue;

        Active = false;
        _controlContainer.Clear(); // remove any controls as the element isnt displaying them anymore
    }

    void OnClicked() {
        Clicked?.Invoke();
    }
}
}