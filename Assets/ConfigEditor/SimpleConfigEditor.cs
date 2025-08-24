using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace ConfigEditor {
// the editor tool will be used to alter the configuration from a .json file
// the complete version will let me add and remove IConfigs, and to set their properties
// the editor will display
//     - preview of what the voro will look like
//     - a column of IConfigs contained in the json
//     - a panel with the properties of a selected IConfig

// v1
// only display the contents of the json to produce the columns of IConfigs
// it is most important to be able to view the objects in the json before anything else
// v2
// make the properties in the objects to be visible, that panel should only display the contents as text
// create an example method that will manually alter the json data, the tool needs the capacity to edit values
// v3
// to preview a voro, use a VoroDemo object and give it the json file that this editor has altered
// a ui button calls ConfigurePointHeight which itself will pass the json into the voro

public class SimpleConfigEditor : EditorWindow {
    [SerializeField] VisualTreeAsset m_VisualTreeAsset = default;

    [MenuItem("Window/UI Toolkit/SimpleConfigEditor")]
    public static void ShowExample() {
        var wnd = GetWindow<SimpleConfigEditor>();
        wnd.titleContent = new GUIContent("SimpleConfigEditor");
    }

    public void CreateGUI() {
        // Each editor window contains a root VisualElement object
        var root = rootVisualElement;

        // the root element contains 3 VisualElements
        // 1) VoroContainer

        // 2) ConfigContainer
        // parse the json to count how many IConfig it contains, create a UI element for each one
        // this UI element is provided that IConfig, and the element sets its name label to match
        // the outcome will display 3 UI elements in the same order as the json

        // 3) PropertyContainer

        // Instantiate UXML
        VisualElement labelFromUXML = m_VisualTreeAsset.Instantiate();
        root.Add(labelFromUXML);
    }
}
}