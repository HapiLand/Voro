using System;
using System.Collections.Generic;
using VoroSystem.Voro.DataStructures.EffectDefinition.Core;
using VoroSystem.Voro.DataStructures.EffectDefinition.ParameterDefinition.Core;
using VoroSystem.Voro.DataStructures.EffectDefinition.ParameterDefinition.Variants;

namespace VoroSystem.Voro.DataStructures.EffectDefinition.Variants {
[EffectVariant(EffectVariants.Noise)]
[Serializable]
public class NoiseEffect : EffectData {
  public NoiseEffect() {
    parameters = new List<ParameterData>
    {
      new("Size", ParameterVariants.FloatField, new FloatValue { value = 1.0f }),
      new("Roughness", ParameterVariants.FloatField, new FloatValue { value = 0.1f })
    };
    effectType = EffectVariants.Noise;
  }
}
}