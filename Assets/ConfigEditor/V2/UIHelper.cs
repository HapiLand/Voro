using Internal;
using UnityEngine.UIElements;

namespace ConfigEditor.V2 {
public static class UIHelper {
    public static VisualElement Create(string name, string className) {
        var ve = new VisualElement { name = name };
        ve.AddToClassList(className);
        return ve;
    }

    /// <summary>
    ///     create a generic display element
    /// </summary>
    /// <param name="name"></param>
    /// <param name="className"></param>
    /// <returns></returns>
    public static VisualElement CreateGenericDisplay(string name) {
        var ve = Create(name, "display");

        var label = new Label(name);
        label.AddToClassList("display-label");
        ve.Add(label);

        return ve;
    }

    /// <summary>
    ///     a input field for a string
    /// </summary>
    /// <param name="name">label name</param>
    /// <returns>an input where text can be written by the user</returns>
    public static VisualElement CreateEffectStringField(string name) {
        // ToDo implement a UXML for string field
        var ve = new VisualElement { name = name };
        ve.AddToClassList("effect-string-field");
        var label = new Label(name);
        label.AddToClassList("display-label");
        ve.Add(label);
        return ve;
    }

    /// <summary>
    ///     a float slider
    /// </summary>
    /// <param name="name">label name</param>
    /// <param name="min">minimum value of slider</param>
    /// <param name="max">maximum value of slider</param>
    /// <param name="defaultValue">initial value of slider</param>
    /// <returns>a slider to set a float value</returns>
    public static VisualElement CreateEffectFloatSlider(string name, float min, float max, float defaultValue) {
        // ToDo implement a UXML for float slider
        // ToDo option for the slider to use a logarithmic scale
        var ve = new Slider($"{name}", min, max);
        ve.name = name;
        ve.value = defaultValue;
        ve.AddToClassList("effect-float-slider");
        return ve;
    }

    /// <summary>
    ///     an integer slider
    /// </summary>
    /// <param name="name">label name</param>
    /// <param name="min">minimum value of slider</param>
    /// <param name="max">maximum value of slider</param>
    /// <param name="defaultValue">initial value of slider</param>
    /// <returns>a slider to set an integer value</returns>
    public static VisualElement CreateEffectIntSlider(string name, int min, int max, int defaultValue) {
        // ToDo implement a UXML for int slider
        // ToDo option for the slider to use a logarithmic scale
        var ve = new Slider($"{name}", min, max);
        ve.name = name;
        ve.value = defaultValue;
        ve.AddToClassList("effect-int-slider");
        return ve;
    }

    public static StyleSheet LoadStyleSheet(string path) {
        return ResourceHelper.LoadResource<StyleSheet>(path);
    }

    // instantiating the uxml puts it into a template container object, this stops that
    // instantiate UXML designed in the UI Builder
    // VisualElement labelFromUXML = m_VisualTreeAsset.Instantiate();
    // while (labelFromUXML.childCount > 0) {
    //     root.Add(labelFromUXML.ElementAt(0));
    // }
}
}