using System.Collections.Generic;
using VoroWorld.Grids;

namespace VoroUI {
public interface IEffect {
    string Name { get; }
    List<ControlElementBase> Controls { get; }
    void Compute(ref WorldTile tile);
}
}