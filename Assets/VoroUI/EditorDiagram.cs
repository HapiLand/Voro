using System.Collections.Generic;
using VoroUI.Elements;

namespace VoroUI {
/// <summary>
///     formerly LayerDiagram
///     EditorDiagram stores a collection of Nodes, this diagram is for choosing the functions to be used
///     as part of the world generation
/// </summary>
public class EditorDiagram {
    /// <summary>
    ///     the layer stores all the EffectElements inside of it
    ///     each EffectElement contains an IEffect
    /// </summary>
    public readonly List<Node> EffectElements = new();

    public readonly string Name;

    public EditorDiagram(string s) {
        Name = s;
    }

    public void AddEffectElement(Node node) {
        EffectElements.Add(node);
    }
}
}