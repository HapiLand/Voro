using System.Collections.Generic;
using VoroUI.Elements.Base;
using VoroWorld.Grids;

namespace VoroUI.Effects {
public interface IEffect {
    string Name { get; }
    List<ControlElementBase> Controls { get; }
    void Compute(ref WorldTile tile);
}
}