using Voro.Jen.Compute.FX.Base;
using Voro.UI.EditorTabs.Nodes;

namespace Voro.Jen.Compute.FX.Internal {
public static class EffectHelper {
    public static IEffect Create(NodeInfo node) {
        var name = node.Name;
        var data = new ConstantHeightData();
        data = node.DataControl.
        
        return name switch
        {
            EffectName.ConstantHeight => new ConstantHeight(data),
            EffectName.Noise => new Noise(new NoiseData()),
            _ => null
        };
    }

    public static INode CreateINode(EffectName name) {
        return name switch
        {
            EffectName.ConstantHeight => new ConstantHeightNode(),
            _ => null
        };
    }
}
}