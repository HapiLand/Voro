using System;
using UnityEngine;
using VoroSystem.VoroWorldGeneration.Map;

namespace VoroSystem.VoroWorldGeneration.HeightSystem {
/// <summary>
/// reads storage, tiles provide access to data
/// =====
/// system manager, owns and serves height data for the world.
/// tiles/other systems query this for height values.
/// ---
/// HeightProvider             produces height data from a source (graph->shader)
/// → TerrainHeightGenerator   invoke height provider to produce height
/// → TerrainHeightSystem      reads storage, tiles provide access to data
/// → TileHeightSampler        TerrainRegion samples stored height to produce float[]
/// → TileEntity               float[] used to displace
/// </summary>
public class TerrainHeightSystem {
  /// <summary>
  /// when TerrainRegion is provided to this,
  /// the generator gets a IHeightProvider to provide height values
  /// </summary>
  readonly TerrainHeightGenerator _generator = new();

  public static Func<Action<Vector3, float>, Vector3[]> SampleHeight(TileEntity tileEntity) {
    // get terrain region for the tile, height will be sampled inside the region
    var sampleRegion = new TerrainRegion(tileEntity);

    // store the region
    TerrainHeightGenerator.StoreRegion(sampleRegion);

    // get all the height providers to access generated height values
    TerrainHeightGenerator.GetProviders(out var providers);

    // get the provided height values, sample within a region to capture generated height
    TerrainHeightGenerator.GenerateHeights(tileEntity, sampleRegion, providers, out var sampled);

    var meshFilter = tileEntity.GetComponent<MeshFilter>();
    var mesh = meshFilter.sharedMesh;
    var vertices = mesh.vertices;
    
    return action => {
      var displacedVertices = new Vector3[vertices.Length];
      
      for (var i = 0; i < vertices.Length; i++) {
        var vtx = vertices[i];

        // Apply sampled height
        var height = sampled[i];
        var displaced = new Vector3(vtx.x, vtx.y + height, vtx.z);
        
        // apply the height to displace
        action?.Invoke(displaced, height);
        displacedVertices[i] = displaced;
      }

      // update mesh with displaced vertices
      mesh.vertices = displacedVertices;
      mesh.RecalculateNormals();
      mesh.RecalculateBounds();
      return displacedVertices;


     
    };
  }

}
}