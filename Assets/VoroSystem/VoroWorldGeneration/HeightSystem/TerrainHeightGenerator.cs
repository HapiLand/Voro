using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using VoroSystem.VoroDataStructures.EffectDefinition.Core;
using VoroSystem.VoroDataStructures.EffectDefinition.Variants;
using VoroSystem.VoroGraphEditor;
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
  /// collection of TerrainRegion in order to calculate the world size
  /// </summary>
  static readonly List<TerrainRegion> _regions = new();

  /// <summary>
  /// adds the terrain region to the generator
  /// when every TileEntity is stored, an accurate size of the world can be found
  /// </summary>
  /// <param name="region"> </param>
  public static void StoreRegion(TerrainRegion region) {
    _regions.Clear(); // todo allow every TileEntity to be stored before calling Action<Vector3, float>
    _regions.Add(region);
  }

  /// <summary>
  /// create every height provider that can access computed world space height values
  /// </summary>
  /// <param name="providers"> provides access to height values in the world </param>
  public static void GetProviders(out List<IHeightProvider> providers) {
    // todo IHeightProvider from Graph,Layer,Effect

    var worldBounds = CalculateWorldBounds(_regions, 2);

    // read the graph and convert its effects to providers
    var graph = GraphScriptableObjectUtility.GetOrCreate();
    // Debug.Log($"Graph: {graph}");

    providers = new List<IHeightProvider>();

    foreach (var layer in graph.layers) {
      foreach (var effect in layer.effects) {
        // Debug.Log($"[Graph {graph.graphName}] [Layer {layer.layerName}] [Effect {effect.effectType.ToString()}]");

        switch (effect) {
        case SlopeEffect slopeEffect: {
          var provider = slopeEffect.GetHeightProvider(worldBounds);
          providers.Add(provider);
        }
          break;
        
        case NoiseEffect noiseEffect: {
          var provider = noiseEffect.GetHeightProvider(worldBounds);
          providers.Add(provider);
        }
          break;
        }
      }
    }
  }

  /// <summary>
  /// generate height values
  /// </summary>
  /// <param name="tileEntity"> </param>
  /// <param name="sampleRegion"> region to sample height within </param>
  /// <param name="providers"> provides computed height values </param>
  /// <param name="sampled"> the sampled height values </param>
  public static void GenerateHeights(
    Tile.TileEntity tileEntity,
    TerrainRegion sampleRegion,
    List<IHeightProvider> providers,
    out float[] sampled) {
    // determine size of array for the sampled floats
    var mesh = tileEntity.GetComponent<MeshFilter>().sharedMesh;
    var vertices = mesh.vertices;
    sampled = new float[vertices.Length];

    // input the terrain region to the provider to get height values
    foreach (var resultArray in providers.Select(provider => provider.ProvideUntyped(sampleRegion, vertices))) {
      switch (resultArray) {
      case float[] floats:
        // accumulate each value into the final sampled array
        for (var i = 0; i < sampled.Length; i++) {
          sampled[i] += floats[i];
        }

        break;
      }
    }
  }

  /// <summary>
  /// find the bounds for every terrain region that is stored
  /// </summary>
  /// <param name="regions"> </param>
  /// <param name="margin"> </param>
  /// <returns> </returns>
  static Bounds CalculateWorldBounds(List<TerrainRegion> regions, float margin = 0f) {
    if (regions == null || regions.Count == 0) {
      return new Bounds(Vector3.zero, Vector3.zero);
    }

    var minX = float.PositiveInfinity;
    var minZ = float.PositiveInfinity;
    var maxX = float.NegativeInfinity;
    var maxZ = float.NegativeInfinity;

    foreach (var region in regions) {
      var halfSize = region.Size * 0.5f;

      var regionMinX = region.Center.x - halfSize;
      var regionMaxX = region.Center.x + halfSize;
      var regionMinZ = region.Center.y - halfSize;
      var regionMaxZ = region.Center.y + halfSize;

      minX = Mathf.Min(minX, regionMinX);
      maxX = Mathf.Max(maxX, regionMaxX);
      minZ = Mathf.Min(minZ, regionMinZ);
      maxZ = Mathf.Max(maxZ, regionMaxZ);
    }

    // apply margin
    minX -= margin;
    minZ -= margin;
    maxX += margin;
    maxZ += margin;

    var min = new Vector3(minX, 0f, minZ);
    var max = new Vector3(maxX, 0f, maxZ);

    var bounds = new Bounds();
    bounds.SetMinMax(min, max);

    return bounds;
  }
}
}