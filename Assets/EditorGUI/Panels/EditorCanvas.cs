using UnityEngine;
using UnityEngine.UIElements;

namespace EditorGUI.Panels {
/// <summary>
///     the core elements of GUI: layers,effects,etc
///     all share the same structure and logic that
///     shall allow for frictionless communication
/// </summary>
[UxmlElement]
public partial class EditorCanvas : VisualElement {
    public EditorCanvas() {
        Debug.Log(this);
        // add heading banner + content
        // add body + content
        // add footer + content
        // set stylesheet for window
        // initialize class component
        // register events
    }
}
}