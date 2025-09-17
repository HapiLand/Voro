using System.Collections.Generic;
using EditorGUI.Source.Utility;
using UnityEngine.UIElements;

namespace EditorGUI.Elements {
/// <summary>
///     when a NodeElement is selected, the EffectData within is displayed in the Inspector panel
///     this element handles the interaction between the user changing its value and the data being updated
///     all inspector elements have the same format, Name-Field-ControlElement
///     ControlElement = eg FloatSlider, ColorPicker, Toggle, LogIntSlider
/// </summary>
[UxmlElement]
public partial class InspectorElement : VisualElement {
    readonly Label _bodyText;

    /// <summary>
    ///     where the control elements are stored
    /// </summary>
    readonly VisualElement _controlContainer;

    public InspectorElement() {
        AddToClassList("panel");
        style.flexGrow = 0;

        _bodyText = UIHelper.Create<Label>("BodyText", "body-text");
        Add(_bodyText);
        _controlContainer = this;

        AssetHelper.LoadAssetPath<StyleSheet>("Assets/EditorGUI/Styles/DiagramElement.uss", OnStyleLoaded);

        DisplayName = "Controls";
    }

    [UxmlAttribute]
    public string DisplayName {
        get => _bodyText?.text ?? "";
        set => _bodyText.text = value;
    }

    public void AddControl(VisualElement element) {
        _controlContainer.Add(element);
    }

    public void SetControls(IEnumerable<VisualElement> controls) {
        foreach (var control in controls) {
            Add(control);
        }
    }

    void OnStyleLoaded(StyleSheet uss) {
        if (uss != null) {
            styleSheets.Add(uss);
        }
    }
}
}