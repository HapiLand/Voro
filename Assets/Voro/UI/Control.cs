using UnityEngine;
using UnityEngine.UIElements;

namespace Voro.UI {
public class ControlElementBase : VisualElement { }

public abstract class Control<TValue> : ControlElementBase {
    readonly Label _label;
    public VisualElement ControlContainer;
    public VisualElement FieldContainer;

    public Control() {
        style.flexDirection = FlexDirection.Row; // horizontal layout
        style.flexGrow = 1; // full size

        // element to store the label 
        var labelContainer = new VisualElement();
        labelContainer.style.flexGrow = 0;
        // labelContainer.style.width = new Length(100, LengthUnit.Pixel); // fixed width
        _label = new Label();
        _label.style.color = Color.black;
        labelContainer.Add(_label);
        Add(labelContainer);

        // element to store the field 
        FieldContainer = new VisualElement();
        FieldContainer.style.width = new Length(50, LengthUnit.Pixel); // fixed width
        Add(FieldContainer);

        // element to store the controls 
        ControlContainer = new VisualElement();
        ControlContainer.style.width = new Length(100, LengthUnit.Pixel); // fixed width
        Add(ControlContainer);
    }

    [UxmlAttribute]
    public string DisplayName {
        get => _label?.text ?? "";
        set => _label.text = value;
    }
}
}