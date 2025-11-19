using System;
using VoroSystem.Compute.EffectSystem.Core;
using VoroSystem.Designer.GraphSystem;

namespace VoroSystem.Compute.EffectSystem {
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