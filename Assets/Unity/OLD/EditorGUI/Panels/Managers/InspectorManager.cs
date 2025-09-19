using EditorGUI.Elements;
using EditorGUI.Source.Effects;
using EditorGUI.Source.Effects.Base;
using UnityEngine.UIElements;

namespace EditorGUI.Panels.Managers {
public class InspectorManager {
    readonly VisualElement _controlContainer;

    public InspectorManager(VisualElement container) {
        _controlContainer = container;
        NodeElement.OnNodeSelectedEvent += OnNodeSelected;
        NodeElement.OnNoSelectedNodes += OnNoSelectedNodes;
    }

    /// <summary>
    ///     a node has been selected
    /// </summary>
    /// <param name="effect"></param>
    void OnNodeSelected(IEffect effect) {
        // remove any controls as the selected node will replace the contents
        RemoveAllControls();

        switch (effect) {
        // add the controls from the node to the inspector
        case SlopeEffect slope:
            _controlContainer.Add(slope.InspectorControls);
            break;
        }
    }

    void RemoveAllControls() {
        _controlContainer.Clear();
    }

    /// <summary>
    ///     no nodes are currently selected
    /// </summary>
    void OnNoSelectedNodes() {
        RemoveAllControls();
    }
}
}