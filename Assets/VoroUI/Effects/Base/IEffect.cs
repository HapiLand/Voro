using System.Collections.Generic;
using VoroUI.Elements.Base;

namespace VoroUI.Effects {
public interface IEffect {
    string Name { get; }
    List<ControlElementBase> Controls { get; }
    void Compute();
}
}