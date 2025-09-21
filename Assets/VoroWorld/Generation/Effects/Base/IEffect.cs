using System.Collections.Generic;
using VoroUI.Elements.Base;

namespace VoroWorld.Generation.Effects.Base {
public interface IEffect {
    string Name { get; }
    List<ControlElementBase> Controls { get; }
    void Compute();
}
}