using UnityEngine;
using VoroSystem.VoroWorldGeneration.HeightSystem;
using VoroSystem.VoroWorldGeneration.Map;

namespace Voro.Internal.Terrain.Algorithms {
public static class AlgorithmDispatcher {
  public static void DispatchOnTile(TileEntity tile) {
    var sampleRegion = new TerrainRegion(tile); // height is sampled within the region
    TerrainHeightGenerator.StoreRegion(sampleRegion);
    TerrainHeightGenerator.GetProviders(out var providers);
    TerrainHeightGenerator.GenerateHeights(tile, sampleRegion, providers, out var sampled);

    var meshFilter = tile.GetComponent<MeshFilter>();
    var mesh = meshFilter.sharedMesh;
    var vertices = mesh.vertices;

    var displacedVertices = new Vector3[vertices.Length];
    for (var i = 0; i < vertices.Length; i++) {
      var vtx = vertices[i];
      DisplaceHeight(sampled, i, vtx, out var displaced);
      displacedVertices[i] = displaced;
    }

    // update mesh with displaced vertices
    mesh.vertices = displacedVertices;
    mesh.RecalculateNormals();
    mesh.RecalculateBounds();
  }

  static void DisplaceHeight(float[] sampled, int i, Vector3 vtx, out Vector3 displaced) {
    var height = sampled[i];
    displaced = new Vector3(vtx.x, vtx.y + height, vtx.z);
  }
}
}