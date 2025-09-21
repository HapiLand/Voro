using UnityEngine.UIElements;
using VoroUI.EditorTabs;
using VoroUI.EditorTabs.Layers;
using VoroUI.EditorTabs.Nodes;

namespace VoroUI {
public class EditorWindow : VisualElement {
    readonly LayersTab _layersTab;
    NodesTab _nodesTab;

    public EditorWindow() {
        // create components
        _layersTab = new LayersTab(out var _nodesTab);

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