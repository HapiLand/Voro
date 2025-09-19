using Source.Utility;
using UnityEngine.UIElements;
using VoroEditor.GUI.Panels;

namespace VoroEditor.GUI {
/// <summary>
///     screw you, unity. I'm taking the name.
///     -
///     editor window is the outer container for the GUI
///     Layer[].Effect[].Control[]
///     this tree of data is stored in a way that is
///     convenient for the GUI to construct it.
///     -
///     EditorWindow outputs an object that contains
///     a collection of IEffect[] which is something
///     VoroCompute depends on
/// </summary>
[UxmlElement]
public partial class EditorWindow : VisualElement {
    /// <summary>
    ///     panel to handle the Layers in use by the editor
    /// </summary>
    readonly Layers LayersPanel;

    public EditorWindow() {
        AddToClassList("background");

        // add the Layers panel so that terrain generation can be designed
        LayersPanel = new Layers();
        Add(LayersPanel);
        AssetUtil.LoadAssetPath<StyleSheet>("Assets/VoroEditor/GUI/StyleSheets/EditorWindowStyle.uss", OnStyleLoaded);

        void OnStyleLoaded(StyleSheet uss) {
            if (uss is null) {
                return;
            }

            styleSheets.Add(uss);
        }
    }
}
}