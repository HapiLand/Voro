using System.Collections.Generic;
using VoroUI.Elements.Base;
using VoroWorld.Diagrams;

namespace VoroUI.EffectsUI.Base {
public interface IEffect {
    string Name { get; }
    List<ControlElementBase> Controls { get; }
}
}