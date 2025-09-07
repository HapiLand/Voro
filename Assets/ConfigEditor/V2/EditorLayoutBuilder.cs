using UnityEngine.UIElements;

namespace ConfigEditor.V2 {
/// <summary>
///     creates all the panels that the GUI has
/// </summary>
public class EditorLayoutBuilder {
    readonly VisualElement _root;

    public EditorLayoutBuilder(VisualElement rootElement) {
        _root = rootElement;
    }

    public void BuildLayout(out VisualElement columnContainer, out VisualElement toolbarPanel,
        out VisualElement inspectorPanel) {
        // create every panel that the GUI has
        var rootPanel = UIHelper.Create("Root", "root");
        var mainPanel = UIHelper.Create("Main", "main");
        inspectorPanel = UIHelper.Create("Inspector", "inspector"); // red
        // ToDo add elements into inspector
        var worldPanel = UIHelper.Create("World", "world"); // orange
        // ToDo display a camera view in the world panel
        var canvasPanel = UIHelper.Create("Canvas", "canvas");

        toolbarPanel = UIHelper.Create("Toolbar", "toolbar");
        columnContainer = UIHelper.Create("ColumnContainer", "column-container"); // yellow
        // ToDo allow each column to write to MyConfig

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