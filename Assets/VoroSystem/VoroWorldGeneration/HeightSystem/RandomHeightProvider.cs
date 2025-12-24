using System;
using UnityEngine;
using Random = System.Random;

namespace VoroSystem.VoroWorldGeneration.HeightSystem {
/// <summary>
/// provides terrain height with random values for height
/// </summary>
public class RandomHeightProvider : IHeightProvider<float> {
  /// <summary>
  /// world bounds where the height will be created inside of
  /// </summary>
  readonly Bounds _worldBounds;

  public RandomHeightProvider(Bounds worldBounds) {
    _worldBounds = worldBounds;
  }

  #region IHeightProvider<float> Members
  public Func<(float x, float z), float> HeightFunc() {
    var rand = new Random();
    return pos => (float)rand.NextDouble();
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
}
}