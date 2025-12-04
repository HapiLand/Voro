using System;
using VoroSystem.Voro.Compute.DiagramSystem;
using VoroSystem.Voro.Compute.EffectSystem.Core;

namespace VoroSystem.Voro.Compute.EffectSystem {
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