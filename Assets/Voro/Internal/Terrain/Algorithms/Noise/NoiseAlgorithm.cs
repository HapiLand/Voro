using System;
using UnityEngine;
using Voro.Internal.Terrain.Attributes;

namespace Voro.Internal.Terrain.Algorithms.Noise {
[Algorithm]
public class NoiseAlgorithm : TerrainAlgorithm {
  #region Serialized Fields
  [ParameterOf(typeof(float), 1.0f)] [HideInInspector]
  public Parameter size;

  [ParameterOf(typeof(float), 0.1f)] [HideInInspector]
  public Parameter roughness;
  #endregion

  #region Event Functions
  void OnEnable() {
    title = "Perlin Noise";
    CollectParameters();
    // heightGenerator = HeightGenerator.Create(LoadShader(),FindShaderName());
    return;

    string FindShaderName() => throw
      // todo get from [Algorithm] 
      new NotImplementedException();

    ComputeShader LoadShader() => throw new NotImplementedException();
  }
  #endregion
}
}