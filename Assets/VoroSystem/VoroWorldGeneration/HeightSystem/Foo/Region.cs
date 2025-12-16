using UnityEngine;

namespace VoroSystem.VoroWorldGeneration.HeightSystem.Foo {
/// <summary>
/// 1x1 square sample at position
/// </summary>
public struct Region {
  /// <summary>
  /// the origin position this sample is located at in the world
  /// </summary>
  public Vector2Int Position;

  /// <summary>
  /// size of the region to sample the height inside, size of the crop area
  /// </summary>
  public readonly float Size;

  /// <summary>
  /// resolution for how many float values the region samples, match mesh resolution
  /// </summary>
  public readonly int Resolution;

  /// <summary>
  /// create a new square region that will crop out height values
  /// </summary>
  /// <param name="position"> world position origin </param>
  /// <param name="resolution"> sample resolution, returns more detail when higher </param>
  public Region(Vector2Int position, int resolution) {
    Position = position;
    Size = 1;
    Resolution = resolution;
  }
}
}