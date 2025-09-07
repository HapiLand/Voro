using UnityEngine;

namespace Terrain {
/// <summary>
///     utility to make creating diagrams easy
/// </summary>
public class DiagramFactory {
    /// <summary>
    ///     a placeholder diagram is used for testing EditorComputer
    ///     this diagram will be populated with data for testing
    /// </summary>
    /// <returns></returns>
    public VoroDiagram CreatePlaceholder() {
        var diagram = new VoroDiagram();

        // populate diagram with data
        diagram.PointMap = new int[1]
        {
            0
        };
        diagram.Points = new (Vector3, int)[1]
        {
            (new Vector3(0, 0, 0), 0)
        };

        return diagram;
    }

    /// <summary>
    ///     this creates a diagram to be used in Tile
    /// </summary>
    /// <returns></returns>
    public VoroDiagram Create() {
        var diagram = new VoroDiagram();
        return diagram;
    }
}
}