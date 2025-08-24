using UnityEngine;
using UnityEngine.UIElements;

namespace ConfigEditor.Source {
// <3   : "Source"
// Yuck : "Scripts"
[UxmlElement]
public partial class ConfigurationElement : VisualElement {
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
}
}