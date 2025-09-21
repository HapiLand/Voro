using UnityEngine.UIElements;
using VoroUI.EditorTabs;
using VoroUI.EditorTabs.Layers;
using VoroUI.EditorTabs.Nodes;

namespace VoroUI {
public class EditorWindow : VisualElement {
    readonly LayersNodesController _controller;
    readonly LayersTab _layersTab;
    readonly NodesTab _nodesTab;

    public EditorWindow() {
        // create components
        _layersTab = new LayersTab();
        _nodesTab = new NodesTab();
        _controller = new LayersNodesController(_layersTab, _nodesTab);


        var cam = new CameraTab();
        // left vertical layout
        var ve = new VisualElement();
        ve.style.flexDirection = FlexDirection.Column; // vertical layout
        ve.style.flexGrow = 1; // full size
        ve.Add(_layersTab);
        ve.Add(_nodesTab);

        // full layout
        style.flexDirection = FlexDirection.Row; // horizontal layout
        style.flexGrow = 1; // full size
        Add(ve);
        Add(cam);
    }
}
}