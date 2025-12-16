using System;
using System.Collections.Generic;
using VoroSystem.Voro.DataStructures.EffectDefinition.Core;
using VoroSystem.Voro.DataStructures.EffectDefinition.ParameterDefinition.Core;
using VoroSystem.Voro.DataStructures.EffectDefinition.ParameterDefinition.Variants;

namespace VoroSystem.Voro.DataStructures.EffectDefinition.Variants {
[EffectVariant(EffectVariants.Slope)]
[Serializable]
public class SlopeEffect : EffectData {
  public SlopeEffect() {
    parameters = new List<ParameterData>
    {
      new("Steepness", ParameterVariants.FloatField, new FloatValue { value = 1.0f }),
      new("Reverse", ParameterVariants.Toggle, new BoolValue { value = false })
    };

    effectType = EffectVariants.Slope;
  }
}
}