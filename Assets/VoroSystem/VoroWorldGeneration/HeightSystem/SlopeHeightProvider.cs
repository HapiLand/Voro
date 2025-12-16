using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using VoroSystem.VoroDataStructures.EffectDefinition.ParameterDefinition.Core;
using VoroSystem.VoroDataStructures.EffectDefinition.ParameterDefinition.Variants;

namespace VoroSystem.VoroWorldGeneration.HeightSystem {
/// <summary>
/// provides terrain height with random values for height
/// </summary>
public class SlopeHeightProvider : IHeightProvider<float> {
  readonly ComputeShader _computeShader;
  readonly int _kernelID;

  /// <summary>
  /// world bounds where the height will be created inside of
  /// </summary>
  readonly Bounds _worldBounds;

  ComputeBuffer _inputBuffer;
  ComputeBuffer _resultBuffer;

  public SlopeHeightProvider(Bounds worldBounds, ComputeShader computeShader) {
    _worldBounds = worldBounds;
    _computeShader = computeShader;
    _kernelID = _computeShader.FindKernel("CSMain");
  }

  #region IHeightProvider<float> Members
  public Func<(float x, float z), float> HeightFunc() {
    return pos => {
      var inputX = new[] { pos.x };
      var inputZ = new[] { pos.z };
      var result = new float[1];

      using (var bufferX = new ComputeBuffer(1, sizeof(float)))
      using (var bufferZ = new ComputeBuffer(1, sizeof(float)))
      using (var bufferResult = new ComputeBuffer(1, sizeof(float))) {
        bufferX.SetData(inputX);
        bufferZ.SetData(inputZ);
        bufferResult.SetData(result);

        _computeShader.SetBuffer(_kernelID, "InputX", bufferX);
        _computeShader.SetBuffer(_kernelID, "InputZ", bufferZ);
        _computeShader.SetBuffer(_kernelID, "Result", bufferResult);

        // _computeShader.SetFloat("Steepness", 1f);
        // _computeShader.SetFloat("Angle", 45f);
        // _computeShader.SetBool("Reverse", false);

        _computeShader.Dispatch(_kernelID, 1, 1, 1);

        bufferResult.GetData(result);
      }

      return result[0];
    };
  }

  Array IHeightProvider.ProvideUntyped(TerrainRegion region, Vector3[] vertices) {
    return Provide(region, vertices);
  }

  public float[] Provide(TerrainRegion region, Vector3[] vertices) {
    var result = new float[vertices.Length];
    var heightFunc = HeightFunc();

    for (var i = 0; i < vertices.Length; i++) {
      var worldX = vertices[i].x + region.Center.x - region.Size / 2f;
      var worldZ = vertices[i].z + region.Center.y - region.Size / 2f;
      result[i] = heightFunc((worldX, worldZ));
    }

    return result;
  }
  #endregion

  public void SetParameters(List<ParameterData> parameters) {
    SetParameter("Direction");
    SetParameter("Steepness");
    SetParameter("Reverse");
    return;

    void SetParameter(string name) {
      var param = parameters.FirstOrDefault(p => p.parameterName == name);
      if (param == null) {
        return;
      }

      switch (param.defaultValue) {
      case FloatValue f:
        _computeShader.SetFloat(name, f.value);
        break;

      case BoolValue b:
        _computeShader.SetBool(name, b.value);
        break;
      }
    }
  }
}
}