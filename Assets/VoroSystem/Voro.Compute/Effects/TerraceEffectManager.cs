using System;
using VoroSystem.Voro.Compute.Effects.Core;
using VoroSystem.Voro.Designer.Canvas;

namespace VoroSystem.Voro.Compute.Effects {
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