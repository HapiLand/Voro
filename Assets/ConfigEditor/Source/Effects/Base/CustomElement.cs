using System;
using Internal;
using UnityEngine.UIElements;

namespace ConfigEditor.Source.Effects.Base {
/// <summary>
///     the base visual element, to be used by all custom effects
/// </summary>
[UxmlElement]
public partial class CustomElement : VisualElement {
    Button _deleteBtn;
    Button _downBtn;
    Button _upBtn;

    public CustomElement() {
        ConfigureContainerObject();
        ConfigureProperties();
        OnInitializeControls();

        // now that the base is constructed, it is returned
        return;
        // following this, each derived class will add their
        // own elements and add them to _controls

        void ConfigureContainerObject() {
            // create and add the UI elements needed to show this element in the Config Editor
            _label = new Label("MyCustomElement");
            _label.name = "Text";
            _label.AddToClassList("element-label");

            _node = new VisualElement();
            _node.name = "Node";
            _node.AddToClassList("element-node");

            Add(_label);
            Add(_node);
            _node.Add(_label);

            // create the buttons so the element order can be changed
            CreateMoveButton(-1, out _upBtn);
            _node.Add(_upBtn); // add to hierarchy
            CreateMoveButton(1, out _downBtn);
            _node.Add(_downBtn); // add to hierarchy

            _deleteBtn = new Button(() => {
                //MoveClicked?.Invoke(this, moveValue);
            });
            _deleteBtn.text = "✖";
            _deleteBtn.name = "Delete";
            _node.Add(_deleteBtn); // add to hierarchy

            // events
            // allow the element to be clicked to select it
            _node.RegisterCallback<MouseDownEvent>(e => Value = !Value);
        }

        void ConfigureProperties() {
            Controls = new VisualElement();
            Controls.name = "Controls";
            Controls.AddToClassList("element-controls");
            Add(Controls);
        }
    }

    #region Effect Configuration

    // these is what the element uses to drive an effect
    // each of the elements (slope,noise,terrace)

    // the container that the properties will go inside
    protected VisualElement Controls { get; private set; }
    // ToDo controls should display the current value of each slider

    #endregion

    public Action<CustomElement, int> MoveClicked { get; set; }

    void CreateMoveButton(int moveValue, out Button btn) {
        btn = new Button(() => {
            // notify the button press
            // 1  = up
            // -1 = down
            MoveClicked?.Invoke(this, moveValue);
        });

        if (moveValue == -1) {
            btn.text = "↑";
            btn.name = "MoveUp";
        }
        else if (moveValue == 1) {
            btn.text = "↓";
            btn.name = "MoveDown";
        }
    }


    protected virtual void OnInitializeControls() { }

    public virtual IConfig ToConfig() {
        throw new NotImplementedException();
    }

    public void RemoveConfigElement() {
        Controls.RemoveFromHierarchy();
    }

    #region Node Element

    // these are what is visible in the config container
    // a clickable box with a label inside that

    // the display name for the element
    [UxmlAttribute]
    public string Text {
        get => _label.text;
        set => _label.text = value;
    }

    Label _label;

    // the background for the label
    VisualElement _node;

    // this element must be clickable, in order for the element to be selected
    bool _value;

    [UxmlAttribute]
    public bool Value {
        get => _value;
        set => Set(value);
    }

    public Action<CustomElement, bool> Selected { get; set; }

    void Set(bool value) {
        _value = value;
        Selected?.Invoke(this, value);
        SetState(value);
    }

    void SetState(bool value) {
        _node.EnableInClassList("element-node_on", value);

        // hide the controls when the effect is not selected
        Controls.EnableInClassList("element-controls_on", value);
        Controls.EnableInClassList("element-controls_off", !value);
    }

    #endregion
}
}

/*
 * every configuration element that can be created in the editor
 * exists as a ConfigurationElement
 *
 * every config requires a path for the menu location where the
 * user can find this when searching for a configuration to
 * add to the ConfigContainer
 *
 * this element is designed to be added into the container, it
 * registers a click which toggles this element as selected
 * it changes color when hovered over
 *
 * this class shall contain a method that gets this as IConfig
 *
 * the configuration contains the variables that go into IConfig
 * when the element is selected, the children inside this
 * are displayed in the PropertyContainer, the user sets the values
 * with several forms of control styles, for different occasions
 * sliders, log sliders, inputs, toggles, ranges, etc.
 *
 * the intent for how the configuration element is so settings
 * and properties to drive the terrain, is to take unnecessary
 * complexity out of the equation, by giving simpler options
 * (not baby simple,
 * but do they 'need' to know what octaves or lacunarity mean?)
 * also due to how voro is designed to function for its purpose
 * these values should be set within a sensible range, in where
 * setting the values should be intuitive based on the way things
 * are named
 *
 * is the term "size" in terms of noise, counter-intuitive?
 * smaller value = smaller noise
 * but actually
 * smaller value = zoom-in the noise
 * although it is technically incorrect, if a tool is going
 * to have a setting, it **sounding** correct has got
 * to be better than if it actually **is** correct
 * anyone who disagrees is a nerd
 */