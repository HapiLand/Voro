using UnityEngine;
using VoroSystem.VoroWorldGeneration.Map;

namespace VoroSystem.VoroWorldGeneration.HeightSystem {
/// <summary>
/// defines a square region (of terrain) in world space.
/// ---
/// tile @ position -> find region of same size ->
/// </summary>
public struct TerrainRegion {
  /// <summary>
  /// center of the region
  /// </summary>
  public Vector2 Center;

  /// <summary>
  /// size of the area to sample
  /// </summary>
  public readonly float Size;

  /// <summary>
  /// mesh resolution
  /// </summary>
  public readonly int Resolution;

  public TerrainRegion(Vector2 center) {
    Center = center;
    Size = WorldGenTileSettings.TileSize;
    Resolution = WorldGenTileSettings.MeshResolution;
  }
}
}