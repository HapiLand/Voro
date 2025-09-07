using ConfigEditor.V2.Effects.Internal;
using Internal;
using Terrain;
using UnityEngine;
using UnityEngine.UIElements;

namespace ConfigEditor.V2 {
/// <summary>
///     computes the result of each column in the editor
///     1) a tile found within UnityComponents.GameWorld is input to EditorCompute
///     the diagram within the tile, a type similar to Voro, its Points are to have the elevation set
///     2) input diagram into compute function, each column does its compute similar to VoroHeight
///     3) compute is completed, the diagram shall be used for Unity Object Instantiation
/// </summary>
public class EditorCompute {
    readonly VisualElement _columnContainer;

    /// <summary>
    ///     temporary diagram for testing
    /// </summary>
    Diagram _placeholderDiagram;
    // 1) a tile found within UnityComponents.GameWorld is input to EditorCompute
    // the diagram within the tile, a type similar to Voro, its Points are to have the elevation set
    // ToDo use the Diagrams that originate from UnityComponents.GameWorld

    public EditorCompute(VisualElement columnContainer) {
        // the container with the columns that each will be computed
        _columnContainer = columnContainer;

        // create the placeholder diagram
        var factory = new DiagramFactory();
        _placeholderDiagram = factory.CreatePlaceholder();
    }


    public void DoCompute() {
        // check if any columns exist
        var columnCount = _columnContainer.childCount;
        if (columnCount == 0) {
            Debug.LogWarning("cannot do compute with 0 columns");
            return;
        }

        Debug.Log($"do EditorCompute with {columnCount} columns");

        // access each column that the editor has
        foreach (var columnChild in _columnContainer.Children()) {
            var columnIndex = _columnContainer.IndexOf(columnChild);
            var column = columnChild;
            Debug.Log($"running compute on column [{columnIndex}] {column.name}");

            // compute the nodes within this column
            ComputeColumn(column, ref _placeholderDiagram);
            
            void ComputeColumn(VisualElement nodeColumn, ref Diagram diagram) {
                // get the element which contains the nodes, these are within a ScrollView
                // this is the vertical list of elements, providing all effects that exist
                
                var scroll = nodeColumn.Q<ScrollView>("NodeScrollView");
                var nodes = scroll.Query<Node>().ToList();

                // check if any nodes exist
                var nodeCount = nodes.Count;
                if (nodeCount == 0) {
                    Debug.LogWarning($"cannot compute, 0 nodes exist in {nodeColumn.name}");
                    return;
                }
                Debug.Log($"computing column [{nodeColumn.name}] which contains {nodeCount} nodes");
                
                // access each individual node within this column, so the diagram can be computed
                foreach (var nodeChild in nodes) {
                    // get the Effect that the node contains
                    var effect = nodeChild.Effect;
                    Debug.Log($"Node Effect {effect.ToString()}");
                    
                    // do compute
                    ComputeEffect(effect, ref diagram);
                }
            }
        }

        // all computing is complete, the diagram is prepared for Unity to perform its Object Instantiation
        Debug.Log("DoCompute completed");
    }

    /// <summary>
    /// performs the compute to modify the diagram, the provided IEffect executes its method
    /// </summary>
    /// <param name="effect">the effect found within the node</param>
    /// <param name="diagram">the diagram that is being computed</param>
    void ComputeEffect(IEffect2 effect, ref Diagram diagram) {
        Debug.Log($"Computing {effect.EffectName} for Diagram {diagram}");
    }

}
}