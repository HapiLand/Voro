using System;
using VoroSystem.Voro.Compute.Effects.EffectSystem.Core;
using VoroSystem.Voro.Designer.Canvas;

namespace VoroSystem.Voro.Compute.Effects.EffectSystem {
[Serializable]
public class NoiseEffectManager : EffectManager {
    public NoiseEffectManager(Node node) {
        Effect = (NoiseEffect)MakeEffect(node);
        Name = "Noise Effect";
    }

    public override string Name { get; }

    protected override IEffect MakeEffect(Node node) {
        var effect = new NoiseEffect();
        effect.Initialize(node);
        return effect;
    }
}
}