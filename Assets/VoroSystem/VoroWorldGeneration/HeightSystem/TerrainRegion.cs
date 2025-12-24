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
  public Vector3 Center;

  /// <summary>
  /// size of the area to sample
  /// </summary>
  public readonly float Size;

  /// <summary>
  /// A region in the world that exists around the bounds of a tile
  /// </summary>
  /// <param name="tileEntity"> </param>
  public TerrainRegion(TileEntity tileEntity) {
    var mesh = tileEntity.GetComponent<MeshFilter>().sharedMesh;
    var meshBounds = mesh.bounds;
    var boundsCenter = meshBounds.center + tileEntity.transform.position;
    Center = boundsCenter;
    Size = WorldGenTileSettings.TileSize;
  }
}
}