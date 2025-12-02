using VoroSystem.Voro.Compute.EffectSystem.Core;

namespace VoroSystem.Voro.Compute.DiagramSystem.Nodes {
public static class NodeFactory {
    public static INode Create(EffectBase.EffectType type) {
        var fields = NodeLookup.LoadEffectFields(type);
        return new Node(type, fields);
    }
}
}