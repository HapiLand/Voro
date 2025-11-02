using System.Collections.Generic;

namespace VoroSystem.World.Generator {
class LandscapeGraph {
    public Dictionary<string, List<int>> BuildGraph() {
        var graph = new Dictionary<string, List<int>>
        {
            { "Foo", new List<int> { 0, 1, 2 } }
        };
        return graph;
    }
}
}