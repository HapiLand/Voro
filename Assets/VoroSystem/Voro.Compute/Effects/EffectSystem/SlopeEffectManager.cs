using System;
using VoroSystem.Voro.Compute.Effects.EffectSystem.Core;
using VoroSystem.Voro.Designer.Canvas;

namespace VoroSystem.Voro.Compute.Effects.EffectSystem {
[Serializable]
public class SlopeEffectManager : EffectManager {
    public SlopeEffectManager(Node node) {
        Effect = (SlopeEffect)MakeEffect(node);
        Name = "Slope Effect";
    }

    public override string Name { get; }

    protected override IEffect MakeEffect(Node node) {
        var effect = new SlopeEffect();
        effect.Initialize(node);
        return effect;
    }
}
}