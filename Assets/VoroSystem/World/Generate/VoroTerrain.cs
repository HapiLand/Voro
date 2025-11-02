using System.Collections.Generic;

namespace VoroSystem.World.Generate {
/// <summary> Represents the physical form of the Environment </summary>
public class VoroTerrain {
    public List<EndResult> Results;

    public VoroTerrain(List<EndResult> endResults) {
        Results = endResults;
    }
}
}