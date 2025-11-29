using System;
using VoroSystem.Voro.Compute.Effects.Core;
using VoroSystem.Voro.Compute.Graphs;

namespace VoroSystem.Voro.Compute.Effects {
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