namespace VoroSystem.Voro.Core.Components.Editor.UIStructure {
/// <summary>
/// define the structure of the UI
/// </summary>
public static class Structure {
    public static LayoutNode CoreLayout = new()
    {
        type = LayoutNode.ElementType.Container,
        id = "editor",
        title = "-- Voro Core --",
        children =
        {
            
        }
    };

    public static LayoutNode DiagramLayout = new()
    {
        type = LayoutNode.ElementType.Container,
        id = "graph",
        title = "-- Voro Diagram --"
    };
    
    public static LayoutNode LayerLayout = new()
    {
        type = LayoutNode.ElementType.Container,
        id = "layer",
        title = "-- Layer --"
    };
    
    public static LayoutNode NodeLayout = new()
    {
        type = LayoutNode.ElementType.Container,
        id = "node",
        title = "-- Effect --"
    };
    
    public static LayoutNode FieldLayout = new()
    {
        type = LayoutNode.ElementType.Container,
        id = "field",
        title = "-- Control --"
    };
}
}