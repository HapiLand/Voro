using System;
using VoroSystem.Voro.Compute.Effects.EffectSystem.Core;
using VoroSystem.Voro.Designer.Graph;

namespace VoroSystem.Voro.Compute.Effects.EffectSystem {
[Serializable]
public class FlatEffectManager : EffectManager {
  public FlatEffectManager(Node node) {
    Effect = (FlatEffect)MakeEffect(node);
    Name = "Flat Effect";
  }

  public override string Name { get; }

  protected override IEffect MakeEffect(Node node) {
    var effect = new FlatEffect();
    effect.Initialize(node);
    return effect;
  }
}
}