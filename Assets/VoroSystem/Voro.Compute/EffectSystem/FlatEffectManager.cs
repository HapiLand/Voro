using System;
using VoroSystem.Voro.Compute.DiagramSystem;
using VoroSystem.Voro.Compute.DiagramSystem.Nodes;
using VoroSystem.Voro.Compute.EffectSystem.Core;

namespace VoroSystem.Voro.Compute.EffectSystem {
[Serializable]
public class FlatEffectManager : EffectManager {
    public FlatEffectManager(INode node) {
        Effect = (FlatEffect)MakeEffect(node);
        Name = "Flat Effect";
    }

    public override string Name { get; }

    protected override IEffect MakeEffect(INode node) {
        var effect = new FlatEffect();
        effect.Initialize(node);
        return effect;
    }
}
}