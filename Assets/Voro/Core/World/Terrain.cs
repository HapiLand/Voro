using System.Collections.Generic;

namespace Voro.Core.World {
/// <summary> Represents the physical form of the Environment </summary>
class Terrain {
    public List<EndResult> Results;

    public Terrain(List<EndResult> endResults) {
        Results = endResults;
    }
}
}