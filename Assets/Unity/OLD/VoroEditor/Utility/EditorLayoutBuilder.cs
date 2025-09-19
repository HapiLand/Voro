using EditorGUI.Source.Utility;
using UnityEngine.UIElements;

namespace OLD.VoroEditor.Utility {
/// <summary>
///     creates all the panels that the GUI has
/// </summary>
public class EditorLayoutBuilder {
    readonly VisualElement _root;

    public EditorLayoutBuilder(VisualElement rootElement) {
        _root = rootElement;
    }

    public void BuildIMGUILayout(out IMGUIContainer layersContainer, out IMGUIContainer toolbarContainer,
        out IMGUIContainer inspectorContainer) {
        var vt = UIHelper.LoadUxml("Window");
        VisualElement ve = vt.Instantiate();
        ve.style.flexGrow = 1;
        _root.Add(ve);
        layersContainer = _root.Q<IMGUIContainer>("Layers");
        toolbarContainer = _root.Q<IMGUIContainer>("Toolbar");
        inspectorContainer = _root.Q<IMGUIContainer>("Inspector");
    }

    public void BuildLayout(out VisualElement columnContainer, out VisualElement toolbarPanel,
        out VisualElement inspectorPanel) {
        // create every panel that the GUI has
        var rootPanel = UIHelper.CreateElement("Root", "root");
        var mainPanel = UIHelper.CreateElement("Main", "main");
        inspectorPanel = UIHelper.CreateElement("Inspector", "inspector"); // red
        var worldPanel = UIHelper.CreateElement("World", "world");
        var canvasPanel = UIHelper.CreateElement("Canvas", "canvas");

        toolbarPanel = UIHelper.CreateElement("Toolbar", "toolbar");
        columnContainer = UIHelper.CreateElement("ColumnContainer", "column-container"); // yellow

        // add the panels into the GUI hierarchy
        _root.Add(rootPanel);
        rootPanel.Add(mainPanel);
        rootPanel.Add(inspectorPanel);
        mainPanel.Add(worldPanel);
        mainPanel.Add(canvasPanel);
        canvasPanel.Add(toolbarPanel);
        canvasPanel.Add(columnContainer);

        // register mouse events for the world panel to animate its size
        worldPanel.RegisterCallback<MouseDownEvent>(_ => worldPanel.AddToClassList("world-hover"));
        worldPanel.RegisterCallback<MouseUpEvent>(_ => worldPanel.RemoveFromClassList("world-hover"));
    }
}
}