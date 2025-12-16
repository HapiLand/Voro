using System;
using System.Collections.Generic;
using VoroSystem.VoroDataStructures.EffectDefinition.Core;
using VoroSystem.VoroDataStructures.EffectDefinition.ParameterDefinition.Core;
using VoroSystem.VoroDataStructures.EffectDefinition.ParameterDefinition.Variants;

namespace VoroSystem.VoroDataStructures.EffectDefinition.Variants {
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