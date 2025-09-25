using System;
using UnityEngine.UIElements;
using Voro.UI.EditorTabs;
using Voro.UI.EditorTabs.Layers;
using Voro.UI.EditorTabs.Nodes;

namespace Voro.UI {
/// <summary>
///     - This is what the user interacts with.
///     - Produces instructions for terrain generation
/// </summary>
public class VoroUI : VisualElement {
    readonly LayersNodesController _controller;
    readonly LayersTab _layersTab;
    readonly NodesTab _nodesTab;
    readonly CameraTab _camTab;


    public VoroUI() {
        _layersTab = new LayersTab();
        _nodesTab = new NodesTab();
        _controller = new LayersNodesController(_layersTab, _nodesTab);

        _camTab = new CameraTab();
        _camTab.ClickedRecompute += () => ClickedRecompute?.Invoke();
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
        Add(_camTab);
    }

    public event Action ClickedRecompute;

    public void Dispose() {
        _camTab.Dispose();
        _camTab.ClickedRecompute -= () => ClickedRecompute?.Invoke(); }
}
}