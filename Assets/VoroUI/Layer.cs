using System.Collections.Generic;

namespace VoroUI {
public class Layer {
    /// <summary>
    ///     the layer stores all the EffectElements inside of it
    ///     each EffectElement contains an IEffect
    /// </summary>
    public readonly List<EffectElement> EffectElements = new();

    public readonly string Name;

    public Layer(string s) {
        Name = s;
    }

    public void AddEffectElement(EffectElement effectElement) {
        EffectElements.Add(effectElement);
    }

}
}