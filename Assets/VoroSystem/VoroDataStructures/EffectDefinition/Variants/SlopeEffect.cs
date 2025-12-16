using System;
using System.Collections.Generic;
using UnityEngine;
using VoroSystem.VoroDataStructures.EffectDefinition.Core;
using VoroSystem.VoroDataStructures.EffectDefinition.ParameterDefinition.Core;
using VoroSystem.VoroDataStructures.EffectDefinition.ParameterDefinition.Variants;
using VoroSystem.VoroWorldGeneration.HeightSystem;

namespace VoroSystem.VoroDataStructures.EffectDefinition.Variants {
[EffectVariant(EffectVariants.Slope)]
[Serializable]
public class SlopeEffect : EffectData {
  public SlopeEffect() {
    parameters = new List<ParameterData>
    {
      new("Steepness", ParameterVariants.FloatField, new FloatValue { value = 1.0f }),
      new("Angle", ParameterVariants.FloatField, new FloatValue { value = 45f }),
      new("Reverse", ParameterVariants.Toggle, new BoolValue { value = false })
    };

    effectType = EffectVariants.Slope;
  }

  public SlopeHeightProvider GetHeightProvider(Bounds worldBounds) {
    var provider = new SlopeHeightProvider(worldBounds, ShaderUtility.Get(EffectVariants.Slope));
    provider.SetParameters(parameters);
    return provider;
  }
}
}