using UnityEngine;
using UnityEngine.UIElements;

namespace EditorGUI.Panels {
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
    public EditorWindow() {
        Debug.Log(this);
    }
}
}