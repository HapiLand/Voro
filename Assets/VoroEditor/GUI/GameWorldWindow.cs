using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace VoroEditor.GUI {
public class GameWorldWindow : EditorWindow {
    [SerializeField] VisualTreeAsset mVisualTreeAsset;
    LayerManager _layerManager;
    ToolbarManager _toolbarManager;

    public void CreateGUI() {
        rootVisualElement.name = "EditorRoot";

        var layoutBuilder = new LayoutBuilder(rootVisualElement);

        layoutBuilder.BuildMainLayout(() => {
            // once the layout is built, add the contents to its containers

            _toolbarManager = new ToolbarManager(rootVisualElement);
            ToolbarManager.OnRefresh += Refresh;
            ToolbarManager.OnCompute += Compute;
            _toolbarManager.BuildToolbar();

            _layerManager = new LayerManager(rootVisualElement);
            // ToDo inspector manager
        });
    }

    void Refresh() { }
    void Compute() { }

    [MenuItem("Voro/Editor")]
    public static void ShowExample() {
        // ToDo menu option to change the layout of unity to the layout the editor uses
        var wnd = GetWindow<GameWorldWindow>();
        wnd.titleContent = new GUIContent("Editor");
    }
}
}