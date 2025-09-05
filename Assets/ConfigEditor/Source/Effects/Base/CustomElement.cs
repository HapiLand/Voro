using System;
using Internal;
using UnityEngine.UIElements;

namespace ConfigEditor.Source.Effects.Base {
/// <summary>
///     the base visual element, to be used by all custom effects
/// </summary>
[UxmlElement]
// ToDo it is too inconvenient to create new effects
//  the process should be simplified, as 4 different files have to be created
//  and several random lines need to also have the effect added to those

// ToDo implement new effects related to game mechanics
// groups -             rules that set a tag for points in certain positions
//                      so that certain effects can only be applied to a certain tag
// smart elevation -    stops the elevation difference between two cells have a value
//                      greater than some amount. can be used to forbid all lethal drops
// flat regions -       locations where the terrain is flat
// ramped steepness -   steepness of elevation increases along a direction
//                      so that the terrain can be shallow at first, then steeper later
// route -              begin at the start of the level, a path runs down the slope
//                      where the elevation along this path is consistent and safe
/*
 * the element/effect workflow will need significant refactoring in order for it to support additional functions
 * -----
 * 1) the editor should be integrated directly with generation
 * create Voro -> execute VoroHeight through ConfigurationEditor -> generate height value
 * NoiseElement/SlopeElement/TerraceElement themselves should Solve height
 *
 * MyConfig.json will be used differently, the controls for the editor (to make an effect, change its properties)
 * will write to the json. the GUI itself will then convert the json into the elements for the UI
 * the way the json is used by VoroHeight is basically how the editor will use it
 *
 * the final height generation will never change, but with this first refactor step, it will allow for
 * easier effect creation, as all I need is to create a new type of CustomElement
 * and less work to solve height, no need for the whole process to be so fractured like it is now
 *
 * 2) height solve must be overhauled
 * the current method to solve height is yucky
 *      for each point { for each effect { solve height } }
 *
 * for the new method, pass the Voro directly to the effect, not each individual position
 * this allows effects to be context aware, Slope 2.0 can set height for the next step neighbor
 * and smart elevation can only exist in a context aware effect
 *
 * and certain effects could be solved on the GPU, something that is easier to do when the effects
 * have access to every point as a collection
 * -----
 * this refactor is not essential now, but the limitations of the current system are starting to show
 *
 * - the planned multi-preset-column idea can only be done when the editor itself is solving height
 * overhauling the GUI will require refactor (1) treat the GUI overhaul as being the new height solver
 *
 * - some important effects and infinite generation are going to require refactor (2), those effects
 * are impossible to develop in this current system and infinite generation will be far too slow unless
 * the height solver is improved
 */
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

            // ToDo delete button needs to remove element
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

    public virtual IConfiguration ToConfig() {
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