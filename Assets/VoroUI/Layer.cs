using System.Collections.Generic;

namespace VoroUI {
public class Layer {
    public readonly string Name;
    List<IEffect> _effects;

    public Layer(string s) {
        Name = s;
    }

    public void AddEffect(IEffect effect) {
        _effects.Add(effect);
    }
}
}