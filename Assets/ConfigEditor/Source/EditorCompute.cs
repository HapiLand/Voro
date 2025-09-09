using UnityEngine;
using UnityEngine.UIElements;

namespace ConfigEditor.Source {
/// <summary>
///     computes the result of each column in the editor
///     1) a tile found within UnityComponents.GameWorld is input to EditorCompute
///     the diagram within the tile, a type similar to Voro, its Points are to have the elevation set
///     2) input diagram into compute function, each column does its compute similar to VoroHeight
///     3) compute is completed, the diagram shall be used for Unity Object Instantiation
/// </summary>
public class EditorCompute {
    readonly VisualElement _columnContainer;

    public EditorCompute(VisualElement columnContainer) {
        // the container with the columns that each will be computed
        _columnContainer = columnContainer;

        // create as singleton instance
        if (Instance != null) {
            return;
        }

        Instance = this;
    }

    public static EditorCompute Instance { get; private set; }

    public void DoCompute(ref WorldTile tile) {
        // check if any columns exist
        var columnCount = _columnContainer.childCount;
        if (columnCount == 0) {
            Debug.LogWarning("cannot do compute with 0 columns");
            return;
        }

        // Debug.Log($"do EditorCompute with {columnCount} columns");

        // access each column that the editor has
        foreach (var columnChild in _columnContainer.Children()) {
            var columnIndex = _columnContainer.IndexOf(columnChild);
            var column = columnChild;
            // Debug.Log($"running compute on column [{columnIndex}] {column.name}");

            // compute the nodes within this column
            ComputeColumn(column, ref tile.Diagram);
            continue;

            void ComputeColumn(VisualElement nodeColumn, ref VoroDiagram voroDiagram) {
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

                // Debug.Log($"computing column [{nodeColumn.name}] which contains {nodeCount} nodes");

                // access each individual node within this column, so the diagram can be computed
                foreach (var nodeChild in nodes) {
                    // do the compute for the node to alter the diagram
                    ComputeEffect(nodeChild, ref voroDiagram);
                }
            }
        }

        // all computing is complete, the diagram is prepared for Unity to perform its Object Instantiation
        Debug.Log("DoCompute completed");
    }

    /// <summary>
    ///     executes the IEffect2 in order to update the value within the diagram
    /// </summary>
    /// <param name="node">the node which contains an effect</param>
    /// <param name="voroDiagram">the diagram that is being computed</param>
    void ComputeEffect(Node node, ref VoroDiagram voroDiagram) {
        // get the Effect that the node contains
        var nodeEffect = node.Effect;
        Debug.Log($"Computing {nodeEffect.EffectName} for Diagram {voroDiagram}");

        // placeholder code, to prove that they are capable of running and altering the diagram
        // while the diagram is a placeholder, the actual IEffect2 functions will be 
        // ToDo compute the diagram all at once, every Point processed together
        // ToDo compute on the GPU

        // compute the effect which will modify the diagram
        nodeEffect.Compute(ref voroDiagram);
    }
}
}