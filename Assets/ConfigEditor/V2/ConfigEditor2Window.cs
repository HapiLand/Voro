using ConfigEditor.V2;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public class ConfigEditor2Window : EditorWindow {
    [SerializeField] VisualTreeAsset m_VisualTreeAsset;

    ColumnManager _columnManager;
    ToolbarManager _toolbarManager;

    public void CreateGUI() {
        // load the style sheet
        rootVisualElement.styleSheets.Add(UIHelper.LoadStyleSheet("config_editor"));

        // build the layout of the GUI
        var layoutBuilder = new EditorLayoutBuilder(rootVisualElement);
        layoutBuilder.BuildLayout(out var columnContainer, out var toolbarPanel);

        // set up the managers for the editor
        var effectNodeFactory = new NodeFactory();
        _columnManager = new ColumnManager(columnContainer, effectNodeFactory);
        _toolbarManager = new ToolbarManager(toolbarPanel, _columnManager, effectNodeFactory);
    }

    // void EffectTest() {
    //     // data which is the configuration for this effect
    // //     // the inspector will display/alter these values
    // var defaultFooData = new NullEffectData
    // {
    //     Foo = 1,
    //     Bar = 2,
    // };
    //     // a new instance of the effect, which was cloned from the dictionary
    //     // this is what the visual element Node shall use
    //     var testEffect = new NullEffect(defaultFooData);
    //     // runs the function of the effect, ie compute height
    //     testEffect.Compute();
    // }

    [MenuItem("Voro/Editor")]
    public static void ShowExample() {
        var wnd = GetWindow<ConfigEditor2Window>();
        wnd.titleContent = new GUIContent("Voro Config Editor");
    }

    // instantiate UXML designed in the UI Builder
    // VisualElement labelFromUXML = m_VisualTreeAsset.Instantiate();
    // while (labelFromUXML.childCount > 0) {
    //     root.Add(labelFromUXML.ElementAt(0));
    // }
}