using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace OLD.TerrainEditorGUI {
public class TerrainEditorWindow : EditorWindow {
    [SerializeField] VisualTreeAsset m_VisualTreeAsset;

    public void CreateGUI() {
        // Each editor window contains a root VisualElement object
        var root = rootVisualElement;

        // VisualElements objects can contain other VisualElement following a tree hierarchy.
        // VisualElement label = new Label("Hello World! From C#");
        // root.Add(label);

        // Instantiate UXML
        VisualElement labelFromUXML = m_VisualTreeAsset.Instantiate();
        labelFromUXML.style.flexGrow = 1; // required as default is set to 0

        root.Add(labelFromUXML);
    }

    [MenuItem("Voro/Show Editor")]
    public static void ShowExample() {
        var wnd = GetWindow<TerrainEditorWindow>();
        wnd.titleContent = new GUIContent("TerrainEditorWindow");
    }
}
}