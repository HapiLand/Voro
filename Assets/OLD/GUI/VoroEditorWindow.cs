using EditorGUI.Source.Utility;
using OLD.GUI.Frames;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace OLD.GUI {
public class VoroEditorWindow : EditorWindow {
    public void CreateGUI() {
        var root = rootVisualElement;
        root.AddToClassList("window-root");

        CreateLayout(
            out var toolbarPanel,
            out var layersPanel,
            out var effectsPanel,
            out var inspectorPanel,
            out var worldPanel);

        var toolbar = new Toolbar();
        var layerMenu = new LayerMenu();
        var effectsMenu = new LayerCanvas();

        var inspector = new InspectorFrame();
        var worldPreview = new WorldPreviewFrame();

        toolbarPanel.Add(toolbar);
        layersPanel.Add(layerMenu);
        effectsPanel.Add(effectsMenu);
        inspectorPanel.Add(inspector);
        worldPanel.Add(worldPreview);

        var stylePath = "Assets/GUI/Styles/GlobalStyle.uss";
        AssetHelper.LoadAssetPath<StyleSheet>(stylePath, OnStyleLoaded);
    }

    [MenuItem("VoroEditor/Show Window")]
    public static void ShowExample() {
        var wnd = GetWindow<VoroEditorWindow>();
        wnd.titleContent = new GUIContent("VoroEditorWindow");
    }

    void CreateLayout(
        out VisualElement toolbarContainer,
        out VisualElement layersSidebar,
        out VisualElement palettePanel,
        out VisualElement inspectorPanel,
        out VisualElement worldPreviewPanel) {
        rootVisualElement.style.flexDirection = FlexDirection.Row;

        var mainEditorColumn = new VisualElement();
        mainEditorColumn.style.flexGrow = 1;
        mainEditorColumn.style.flexDirection = FlexDirection.Column;

        var mainContentRow = new VisualElement();
        mainContentRow.style.flexDirection = FlexDirection.Row;

        // Create UI containers
        worldPreviewPanel = UIHelper.CreateElement("WorldPreviewContainer", "frame-container");
        toolbarContainer = UIHelper.CreateElement("ToolbarContainer", "frame-container");
        layersSidebar = UIHelper.CreateElement("LayersSidebar", "frame-container");
        palettePanel = UIHelper.CreateElement("PalettePanel", "frame-container");
        inspectorPanel = UIHelper.CreateElement("InspectorPanel", "frame-container");

        // Build hierarchy
        rootVisualElement.Add(mainEditorColumn);
        rootVisualElement.Add(worldPreviewPanel);
        mainEditorColumn.Add(toolbarContainer);
        mainEditorColumn.Add(mainContentRow);
        mainContentRow.Add(layersSidebar);
        mainContentRow.Add(palettePanel);
        mainContentRow.Add(inspectorPanel);
    }

    void OnStyleLoaded(StyleSheet uss) {
        if (uss != null) {
            rootVisualElement.styleSheets.Add(uss);
        }
    }
}
}