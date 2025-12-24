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

  public bool Contains(Vector3 worldPosition) {
    return Bounds.Contains(worldPosition);
  }
}
}