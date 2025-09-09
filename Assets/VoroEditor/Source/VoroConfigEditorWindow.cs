using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;
using VoroEditor.Utility;

namespace VoroEditor.Source {
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

    /// <summary>
    ///     allows for interaction between the editor and the game world
    ///     this is in order for the editor to process all the diagrams in the world
    /// </summary>
    WorldManager _worldManager;

    void Update() {
        // ToDo GameWorld and the EditorWindow need to communicate
        //  this is in order for the diagrams that exist in the world can be computed
    }

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
        _worldManager = WorldManagerFactory.GetWorldManager();

        // add default content to the editor
        SetDefaults();
    }

    /// <summary>
    ///     set default elements in the GUI, so that it begins with a column already added
    /// </summary>
    void SetDefaults() {
        // add the column and select it right away
        _columnManager.AddColumn(true);
    }

    [MenuItem("Voro/Editor")]
    public static void ShowExample() {
        var wnd = GetWindow<VoroConfigEditorWindow>();
        wnd.titleContent = new GUIContent("Voro Config Editor");
    }
}
}