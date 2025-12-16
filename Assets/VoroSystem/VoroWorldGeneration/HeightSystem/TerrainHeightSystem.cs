using System;
using UnityEngine;
using VoroSystem.Voro.Utilities.Extensions;
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
/// → TerrainHeightStorage     stores height in its format (volume?)
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
  
  // todo implement RandomHeightProvider
  // todo implement generation method, TerrainHeightGenerator invokes IHeightProvider
  // todo implement TerrainHeightStorage to store height values
  // todo implement TileHeightSampler to retreive stored height to produce float[]

  public Func<Action<Vector3, float>, Vector3[]> SampleRegion(Tile.TileEntity tileEntity) {
    
    _generator.Begin(tileEntity, out var vertices);

    return action => {
      var result = new Vector3[vertices.Length];
      for (var i = 0; i < vertices.Length; i++) {
        var vtx = vertices[i];
        // find the height value
        var height = 0.0f;
        var position = new Vector3(vtx.x, vtx.y + height, vtx.z);
        action?.Invoke(position, height);
        result[i] = position;
      }

      var mesh = tileEntity.GetComponent<MeshFilter>().sharedMesh;  
      mesh.vertices = result;
      mesh.RecalculateNormals();
      mesh.RecalculateBounds();
      return result;

      /*// get bounds of the sample region, find height within this area
      var (minX, minZ, maxX, maxZ) = _storage.GetSampleBounds(_region);
      var countX = maxX - minX + 1;
      var countZ = maxZ - minZ + 1;
      // the sampled world heights are stored in this array
      var sampledHeightResults = new float[countX * countZ];
      var index = 0;
      for (var dx = minX; dx <= maxX; dx++) {
        for (var dz = minZ; dz <= maxZ; dz++) {
          var height = _storage.SampleHeightBilinear(dx, dz, _region.Resolution);
          sampledHeightResults[index++] = height;
        }
      }*/


      // old
      // var (minX, minZ, maxX, maxZ) = _storage.GetSampleBounds(_region);
      // var worldScale = _storage.StepSize / _region.Resolution;
      // for (var dx = minX; dx <= maxX; dx++) {
      //   for (var dz = minZ; dz <= maxZ; dz++) {
      //     var height = _storage.SampleHeightBilinear(dx, dz, _region.Resolution);
      //     var pos = new Vector3(dx * worldScale, 0f, dz * worldScale);
      //     action(pos, height);
      //   }
      // }


      // new
      /*var (minX, minZ, maxX, maxZ) = _storage.GetSampleBounds(_region);
      var worldScale = _storage.StepSize / _region.Resolution;

      var countX = maxX - minX + 1;
      var countZ = maxZ - minZ + 1;
      var result = new Vector3[countX * countZ];

      var index = 0;
      for (var dx = minX; dx <= maxX; dx++) {
        for (var dz = minZ; dz <= maxZ; dz++) {
          var height = _storage.SampleHeightBilinear(dx, dz, _region.Resolution);
          var pos = new Vector3(dx * worldScale, height, dz * worldScale);

          action?.Invoke(pos, height);
          result[index++] = pos;
        }
      }*/
    };
  }

  // public void SampleRegion(Action<Vector3, float> action) {
  //   var (minX, minZ, maxX, maxZ) = _storage.GetSampleBounds(_region);
  //   var worldScale = _storage.StepSize / _region.Resolution;
  //   for (var dx = minX; dx <= maxX; dx++) {
  //     for (var dz = minZ; dz <= maxZ; dz++) {
  //       var height = _storage.SampleHeightBilinear(dx, dz, _region.Resolution);
  //       var pos = new Vector3(dx * worldScale, 0f, dz * worldScale);
  //       action(pos, height);
  //     }
  //   }
  // }
}
}