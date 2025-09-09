using UnityEngine.UIElements;

namespace ConfigEditor.Source {
/// <summary>
///     utility for constructing a node
/// </summary>
public class NodeFactory {
    VisualElement _selectedNode;

    /// <summary>
    ///     creates a new effect
    /// </summary>
    /// <param name="effectName">the name of the effect</param>
    /// <returns>the visual element of the node effect</returns>
    public Node Create(string effectName) {
        var node = Node.CreateInstance(effectName);
        return node;
    }
}
}