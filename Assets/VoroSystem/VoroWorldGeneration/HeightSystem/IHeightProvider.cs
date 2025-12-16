using System;
using UnityEngine;

namespace VoroSystem.VoroWorldGeneration.HeightSystem {
/// <summary>
/// produces height data from a source (graph->shader)
/// =====
/// represents a producer of terrain height data that writes into the central terrain height system.
/// ---
/// implementations generate or modify height values for a given world-space region.
/// (compute shaders, CPU generation, or procedural modifiers).
/// ---
/// this interface is used only during terrain generation or updates.
/// this is never queried directly by tiles or meshes.
/// ---
/// responsibility is to contribute height data to terrain storage
/// it is not used to query height values, it does not store terrain data
/// </summary>
public interface IHeightProvider<T> : IHeightProvider {
  /// <summary>
  /// samples a height value at a coordinate
  /// </summary>
  /// <returns> </returns>
  Func<(float x, float z), T> HeightFunc();

  /// <summary>
  /// provides array of height values sampled within the region
  /// </summary>
  /// <param name="region"> </param>
  /// <param name="vertices"> </param>
  /// <returns> </returns>
  T[] Provide(TerrainRegion region, Vector3[] vertices);
}

public interface IHeightProvider {
  Array ProvideUntyped(TerrainRegion region, Vector3[] vertices);
}
}