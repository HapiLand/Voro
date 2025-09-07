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
    public Diagram CreatePlaceholder() {
        var diagram = new Diagram();
        // ToDo add data to diagram
        return diagram;
    }

    /// <summary>
    ///     this creates a diagram to be used in Tile
    /// </summary>
    /// <returns></returns>
    public Diagram Create() {
        var diagram = new Diagram();
        return diagram;
    }
}
}