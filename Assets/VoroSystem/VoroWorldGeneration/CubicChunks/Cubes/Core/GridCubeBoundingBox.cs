using UnityEngine;
using VoroSystem.VoroWorldGeneration.CubicChunks.World.Core;

namespace VoroSystem.VoroWorldGeneration.CubicChunks.Cubes.Core {
public class GridCubeBoundingBox {
  readonly Transform _transform;

  public GridCubeBoundingBox(Transform transform) {
    _transform = transform;
  }

  public Vector3Int GridCoord { get; set; }
  public Bounds Bounds => new(_transform.position, Vector3.one * WorldSettings.GridSize);

  /// <summary>
  /// world space position of the bottom-left point
  /// </summary>
  public Vector3 WorldOriginPosition => new(
    GridCoord.x * WorldSettings.GridSize,
    GridCoord.y * WorldSettings.GridSize,
    GridCoord.z * WorldSettings.GridSize);

  public Vector3Int BoundSize => new(
    Mathf.Max(1, Mathf.CeilToInt(Bounds.size.x)),
    Mathf.Max(1, Mathf.CeilToInt(Bounds.size.y)),
    Mathf.Max(1, Mathf.CeilToInt(Bounds.size.z))
  );

  public bool Contains(Vector3 worldPosition) {
    return Bounds.Contains(worldPosition);
  }
}
}