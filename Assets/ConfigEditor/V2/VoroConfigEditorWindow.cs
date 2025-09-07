using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace ConfigEditor.V2 {
public class VoroConfigEditorWindow : EditorWindow {
    [SerializeField] VisualTreeAsset mVisualTreeAsset;

    /// <summary>
    ///     ensures that the editor interacts with the correct column
    ///     each column will be used like a set of layers
    ///     --
    ///     example
    ///     Column1 > MyConfig1.json
    ///     Column2 > MyConfig2.json
    ///     Column3 > MyConfig3.json
    ///     --
    ///     when the editor computes, each node for every column is used on the voro
    /// </summary>
    ColumnManager _columnManager;

    /// <summary>
    ///     computes every effect to find the elevation
    /// </summary>
    EditorCompute _editorCompute;

    /// <summary>
    ///     manages the EffectData of a selected node, ensuring the correct node is interacted with
    ///     will update the values within the effect
    /// </summary>
    InspectorManager _inspectorManager;

    /// <summary>
    ///     populates dropdown menus with effects that the user can choose to add to a column
    /// </summary>
    ToolbarManager _toolbarManager;

    public void CreateGUI() {
        // load the style sheet
        rootVisualElement.styleSheets.Add(UIHelper.LoadStyleSheet("config_editor"));

        // build the layout of the GUI
        var layoutBuilder = new EditorLayoutBuilder(rootVisualElement);
        layoutBuilder.BuildLayout(out var columnContainer, out var toolbarPanel, out var inspectorPanel);

        // set up the managers for the editor
        var effectNodeFactory = new NodeFactory();
        _editorCompute = new EditorCompute(columnContainer);
        _columnManager = new ColumnManager(columnContainer, effectNodeFactory);
        _toolbarManager = new ToolbarManager(toolbarPanel, _columnManager, effectNodeFactory, _editorCompute);
        _inspectorManager = new InspectorManager(inspectorPanel);
    }

    [MenuItem("Voro/Editor")]
    public static void ShowExample() {
        var wnd = GetWindow<VoroConfigEditorWindow>();
        wnd.titleContent = new GUIContent("Voro Config Editor");
    }

    // instantiate UXML designed in the UI Builder
    // VisualElement labelFromUXML = m_VisualTreeAsset.Instantiate();
    // while (labelFromUXML.childCount > 0) {
    //     root.Add(labelFromUXML.ElementAt(0));
    // }
}
}