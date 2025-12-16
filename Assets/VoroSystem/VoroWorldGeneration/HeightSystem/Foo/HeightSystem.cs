using System;
using UnityEngine;

namespace VoroSystem.VoroWorldGeneration.HeightSystem.Foo {
/// <summary>
/// owns storage, serves height data
/// </summary>
public class HeightSystem {
  /// <summary>
  /// stores all the height values in the world
  /// </summary>
  readonly HeightStorage _storage = new(10, 1f);

  public void SampleRegion(Region region, Action<Vector3, float> action) {
    var (minX, minZ, maxX, maxZ) = _storage.GetSampleBounds(region);
    var worldScale = _storage.StepSize / region.Resolution;

    for (var dx = minX; dx <= maxX; dx++) {
      for (var dz = minZ; dz <= maxZ; dz++) {
        var height = _storage.SampleHeightBilinear(dx, dz, region.Resolution);
        var pos = new Vector3(dx * worldScale, 0f, dz * worldScale);
        action(pos, height);
      }
    }
  }

  /*public float StepSize => _storage.StepSize;

  public void ForEach(Action<(int, int), float> action) {
    var heightMap = _storage.GetHeightMap();
    var sizeX = heightMap.GetLength(0);
    var sizeZ = heightMap.GetLength(1);
    for (var x = 0; x < sizeX; x++) {
      for (var z = 0; z < sizeZ; z++) {
        action((x, z), heightMap[x, z]);
      }
    }
  }*/
}
}