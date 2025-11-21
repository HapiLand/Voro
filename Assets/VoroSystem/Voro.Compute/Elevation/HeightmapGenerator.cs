using System;
using System.Collections.Generic;
using UnityEngine;
using VoroSystem.Voro.Compute.Effects.EffectSystem;
using VoroSystem.Voro.Compute.Effects.EffectSystem.Core;
using VoroSystem.Voro.Designer.Graph;
using VoroSystem.Voro.World.TerrainOLD.Ground.Chunks;

namespace VoroSystem.Voro.Compute.Elevation {
/// <summary>
/// Generates a heightmap texture using a compute shader
/// </summary>
[Serializable]
public class HeightmapGenerator {
  public Texture2D HandleDoCompute(Graph graph, ChunkInstance instance) {
    var dictionary = ReadGraphDictionary(graph, out var allowCompute);

    var result = Texture2D.blackTexture;

    if (!allowCompute) {
      return result;
    }

    // each Layer
    foreach (var (layerName, list) in dictionary) {
      Debug.Log($"Layer \"{layerName}\"");
      // each Effect Manager
      list.ForEach(effect => {
        Debug.Log($"Computing Effect \"{effect.Name}\"");
        result = effect.RunEffect(instance);
      });
    }

    return result;
  }

  /// <summary>
  /// get the data from the Graph to find the Effects it stores.
  /// Dictionary key is the Layer, value are its Effects
  /// </summary>
  Dictionary<string, List<EffectManager>> ReadGraphDictionary(Graph graph, out bool allowCompute) {
    var layerCount = graph.layers.Count;
    var graphDictionary = new Dictionary<string, List<EffectManager>>();

    if (layerCount == 0) {
      Debug.Log("No Layers found in Graph");
      allowCompute = false;
      return graphDictionary;
    }

    Debug.Log($"Reading {layerCount} Layers");
    for (var i = 0; i < layerCount; i++) {
      var layer = graph.layers[i];
      var layerName = layer.layerName;

      // add each unique layer to the dictionary
      if (!graphDictionary.TryGetValue(layerName, out var effectList)) {
        effectList = new List<EffectManager>();
        graphDictionary[layerName] = effectList;
      }

      // read the Nodes, create the matching Effect
      foreach (var node in layer.nodes) {
        switch (node.nodeName) {
        case "Slope": {
          effectList.Add(new SlopeEffectManager(node));
          break;
        }
        case "Flat": {
          effectList.Add(new FlatEffectManager(node));
          break;
        }
        case "Noise": {
          effectList.Add(new NoiseEffectManager(node));
          break;
        }
        case "Terrace": {
          effectList.Add(new TerraceEffectManager(node));
          break;
        }
        }
      }
    }

    allowCompute = true;
    return graphDictionary;
  }
}
}