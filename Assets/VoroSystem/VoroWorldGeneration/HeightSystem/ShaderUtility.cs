using UnityEditor;
using UnityEngine;
using VoroSystem.VoroDataStructures.EffectDefinition.Core;

namespace VoroSystem.VoroWorldGeneration.HeightSystem {
public static class ShaderUtility {
  public static ComputeShader Get(EffectVariants variant) {
    var cs = variant switch
    {
      EffectVariants.Slope => AssetDatabase.LoadAssetAtPath<ComputeShader>("Assets/" +
                                                                           ShaderEditorPaths
                                                                             .SLOPE_EFFECT_RELATIVE_PATH),
      EffectVariants.Noise => AssetDatabase.LoadAssetAtPath<ComputeShader>("Assets/" +
                                                                           ShaderEditorPaths
                                                                             .NOISE_EFFECT_RELATIVE_PATH),
      _ => null
    };
    return cs;
  }
}
}