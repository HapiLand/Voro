using System;
using System.Linq;
using EditorGUI.Source.Utility;
using OLD.VoroEditor.Effects.Internal.enums;
using UnityEngine.UIElements;

namespace OLD.VoroEditor.Elements {
[UxmlElement]
public partial class TypeDropdown : VisualElement {
    readonly DropdownField _field;
    readonly Label _label;

    Func<ComputeTypes> _getter;
    Action<ComputeTypes> _setter;

    public TypeDropdown() {
        // instantiate the uxml
        var vt = UIHelper.LoadUxml("TypeDropdown");
        vt.CloneTree(this);

        // query each element
        _label = this.Q<Label>("Label");
        _field = this.Q<DropdownField>("Dropdown");

        var enumNames = Enum.GetNames(typeof(ComputeTypes)).ToList();
        _field.choices = enumNames;

        // Register change callback
        _field.RegisterValueChangedCallback(evt => {
            if (Enum.TryParse(evt.newValue, out ComputeTypes selected)) {
                _setter?.Invoke(selected);
            }
        });
    }

    #region UXML Attributes

    [UxmlAttribute]
    public string DisplayName {
        get => _label?.text ?? "";
        set => _label.text = value;
    }

    #endregion

    /// <summary>
    ///     bind the dropdown to the external data
    /// </summary>
    public void Bind(Func<ComputeTypes> getter, Action<ComputeTypes> setter) {
        _getter = getter;
        _setter = setter;
        Refresh();
    }

    /// <summary>
    ///     refresh the dropdown whenever the data changes externally
    /// </summary>
    public void Refresh() {
        if (_getter != null) {
            _field.SetValueWithoutNotify(_getter().ToString());
        }
    }
}
}