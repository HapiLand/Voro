using EditorGUI.Source.Utility;
using UnityEngine.UIElements;

namespace EditorGUI.Elements.Internal {
/// <summary>
///     this is a base class for the inspector controls
///     ControlElements is a generic class so that InspectorElement can be designed to use a range of controls
/// </summary>
[UxmlElement]
public abstract partial class ControlElement<TValue> : VisualElement {
    readonly Label _bodyText;
    public readonly VisualElement _controlContainer; // stores the control element
    public readonly VisualElement _controlsField; // field to display the value of the controls

    public ControlElement() {
        AddToClassList("panel");
        style.flexGrow = 0;
        style.flexDirection = FlexDirection.Row;

        _bodyText = UIHelper.Create<Label>("BodyText", "body-text");
        Add(_bodyText);

        _controlContainer = UIHelper.Create<VisualElement>("ControlsContainer", "body");
        _controlsField = UIHelper.Create<VisualElement>("ControlsField", "body");

        Add(_controlsField);
        Add(_controlContainer);
        
        // Create the derived-specific field
        //     Field = CreateField();
        //     Field.name = "InputField";
        //     Field.AddToClassList("input-field");
        //     Field.RegisterValueChangedCallback(OnValueChanged);
        //     Add(Field);

        AssetHelper.LoadAssetPath<StyleSheet>("Assets/EditorGUI/Styles/Button.uss", OnStyleLoaded);
    }


    //
    // protected BaseField<TValue> Field { get; }
    //
    [UxmlAttribute]
    public string DisplayName {
        get => _bodyText?.text ?? "";
        set => _bodyText.text = value;
    }

    //
    // [UxmlAttribute]
    // public TValue Value {
    //     get => Field?.value ?? default;
    //     set
    //     {
    //         if (Field != null) {
    //             Field.value = value;
    //         }
    //     }
    // }
    //
    // /// <summary>
    // ///     the actual field control (log slider, int slider, log float slider, etc)
    // ///     the control is provided to the derived class
    // /// </summary>
    // protected abstract BaseField<TValue> CreateField();
    //
    void OnStyleLoaded(StyleSheet uss) {
        if (uss != null) {
            styleSheets.Add(uss);
        }
    }
    //
    // void OnValueChanged(ChangeEvent<TValue> evt) {
    //     Value = evt.newValue;
    //     OnValueChangedEvent?.Invoke(evt.newValue);
    // }
    //
    // /// <summary>
    // ///     call when value is changed in the GUI
    // /// </summary>
    // public event Action<TValue> OnValueChangedEvent;
}
}