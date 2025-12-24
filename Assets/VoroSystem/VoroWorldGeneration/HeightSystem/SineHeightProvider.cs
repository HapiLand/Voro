using System;
using UnityEngine;

namespace VoroSystem.VoroWorldGeneration.HeightSystem {
/// <summary>
/// provides terrain height with random values for height
/// </summary>
public class SineHeightProvider : IHeightProvider<float> {
  readonly ComputeShader _computeShader;
  readonly int _kernelID;

  /// <summary>
  /// world bounds where the height will be created inside of
  /// </summary>
  readonly Bounds _worldBounds;

  ComputeBuffer _inputBuffer;
  ComputeBuffer _resultBuffer;

  public SineHeightProvider(Bounds worldBounds, ComputeShader computeShader) {
    _worldBounds = worldBounds;
    _computeShader = computeShader;
    _kernelID = _computeShader.FindKernel("CSMain");
  }

  #region IHeightProvider<float> Members
  public Func<(float x, float z), float> HeightFunc() {
    return pos => {
      // Allocate buffers for a single value
      var input = new[] { pos.x };
      var result = new float[1];

      _inputBuffer = new ComputeBuffer(1, sizeof(float));
      _resultBuffer = new ComputeBuffer(1, sizeof(float));

      _inputBuffer.SetData(input);
      _computeShader.SetBuffer(_kernelID, "InputX", _inputBuffer);
      _computeShader.SetBuffer(_kernelID, "Result", _resultBuffer);

      _computeShader.Dispatch(_kernelID, 1, 1, 1);

      _resultBuffer.GetData(result);

      _inputBuffer.Release();
      _resultBuffer.Release();

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
      var worldZ = vertices[i].z + region.Center.z - region.Size / 2f;
      result[i] = heightFunc((worldX, worldZ));
    }

    return result;
  }
  #endregion
}
}