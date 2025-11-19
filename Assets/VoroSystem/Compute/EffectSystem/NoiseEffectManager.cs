using System;
using VoroSystem.Compute.EffectSystem.Core;
using VoroSystem.Designer.GraphSystem;

namespace VoroSystem.Compute.EffectSystem {
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