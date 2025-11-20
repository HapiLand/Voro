using UnityEngine;

namespace VoroSystem.Voro.World.Landscape.Map {
public class WorldGrid {
  #region Serialized Fields

  [SerializeField] [Range(0.1f, 1f)] float gridSize = 1f;
  [SerializeField] WorldBoundary worldBoundary;

  #endregion

  public float GridSize => gridSize;

  public (int xSize, int zSize, float gridSize) Dimensions {
    get
    {
      var size = worldBoundary.Size;
      var xCells = Mathf.Max(1, Mathf.RoundToInt(size.xSize / gridSize));
      var zCells = Mathf.Max(1, Mathf.RoundToInt(size.zSize / gridSize));
      return (xCells, zCells, gridSize);
    }
  }

  public Vector3 Origin => worldBoundary.Corner.A;

  public void Initialize(WorldBoundary boundary) {
    worldBoundary = boundary;
  }
}
}