using UnityEngine;
using VoroSystem.Voro.Utilities.Extensions;
using VoroSystem.VoroWorldGeneration.Map;

namespace VoroSystem.VoroWorldGeneration.HeightSystem {
/// <summary>
/// invoke height provider to produce height
/// =====
/// orchestrates height generation by invoking one or more IHeightProvider instances.
/// writes results into TerrainHeightStorage.
/// </summary>
public class TerrainHeightGenerator {
  
  /// <summary>
  /// upon generating height values, these are kept in storage
  /// </summary>
  readonly TerrainHeightStorage _storage = new();

  /// <summary>
  /// generates height values to be captured by a region
  /// </summary>
  /// <param name="tileEntity"></param>
  /// <param name="vertices">world space positions to sample height at</param>
  public void Begin(Tile.TileEntity tileEntity, out Vector3[] vertices) {
    // todo create height values, return height values so TerrainHeightSystem can sample them
    
    // get the mesh in the tile
    var mesh = tileEntity.GetComponent<MeshFilter>().sharedMesh;
    vertices = mesh.vertices;

    // make the terrain region for this mesh, this will find the world height values
    var bounds = mesh.bounds;
    var center = bounds.center + tileEntity.transform.position;
    var region = new TerrainRegion(center.ToVector2());
  }
}
}