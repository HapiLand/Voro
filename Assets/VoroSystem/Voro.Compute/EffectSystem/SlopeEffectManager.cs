using System;
using VoroSystem.Voro.Compute.DiagramSystem;
using VoroSystem.Voro.Compute.DiagramSystem.Nodes;
using VoroSystem.Voro.Compute.EffectSystem.Core;

namespace VoroSystem.Voro.Compute.EffectSystem {
[Serializable]
public class SlopeEffectManager : EffectManager {
    public SlopeEffectManager(INode node) {
        Effect = (SlopeEffect)MakeEffect(node);
        Name = "Slope Effect";
    }

    public override string Name { get; }

    protected override IEffect MakeEffect(INode node) {
        var effect = new SlopeEffect();
        effect.Initialize(node);
        return effect;
    }
}
}