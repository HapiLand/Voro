using System.Collections.Generic;
using Voro.UI;
using Voro.World;

namespace Voro.Jen.Effects {
public interface IEffect {
    string Name { get; }
    List<ControlElementBase> Controls { get; }
    void Compute(ref Chunk diagram);
}
}