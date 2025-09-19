using System.Collections.Generic;
using VoroUI.Elements;

namespace VoroUI {
/// <summary>
///     related to VoroDiagram as this is what is used for it to be computed
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