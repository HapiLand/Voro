using System;
using UnityEngine;
using VoroSystem.VoroWorldGeneration.CubicChunks.World.Core;

namespace VoroSystem.VoroWorldGeneration.CubicChunks.Cubes.Core {
[Serializable]
public class GridCubeBoundingBox {
  #region Serialized Fields
  [field: SerializeField] public Vector3Int GridCoord { get; set; }

  [SerializeField] Transform transform;
  #endregion

  public GridCubeBoundingBox(Transform transform) {
    this.transform = transform;
  }

  public Bounds Bounds => new(transform.position, Vector3.one * WorldSettings.GridSize);

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

  public bool Contains(Vector3 worldPosition) => Bounds.Contains(worldPosition);
}
}