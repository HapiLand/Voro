using System;
using VoroSystem.Compute.EffectSystem.Core;
using VoroSystem.Designer.GraphSystem;

namespace VoroSystem.Compute.EffectSystem {
[Serializable]
public class TerraceEffectManager : EffectManager {
  public TerraceEffectManager(Node node) {
    Effect = (TerraceEffect)MakeEffect(node);
    Name = "Terrace Effect";
  }

  public override string Name { get; }

  protected override IEffect MakeEffect(Node node) {
    var effect = new TerraceEffect();
    effect.Initialize(node);
    return effect;
  }
}
}