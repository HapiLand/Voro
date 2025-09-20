using System.Collections.Generic;
using VoroUI.Elements.Base;
using VoroWorld;

namespace VoroUI.Effects.Base {
public interface IEffect {
    string Name { get; }
    List<ControlElementBase> Controls { get; }
    void Compute(ref VoroDiagram diagram);
}
}