using System.Collections.Generic;
using UnityEngine;
using VoroSystem.Voro.Compute.Components;
using VoroSystem.Voro.Compute.DiagramSystem;
using VoroSystem.Voro.Compute.EffectSystem;
using VoroSystem.Voro.Compute.EffectSystem.Core;
using VoroSystem.Voro.World;
using VoroSystem.Voro.World.ChunkStructure;

namespace VoroSystem.Voro.Core {
[ExecuteAlways]
public class VoroCore : MonoBehaviour {
  #region Serialized Fields

  [SerializeField] VoroWorld voroWorld;
  [SerializeField] VoroCompute voroCompute;

  #endregion

  #region Event Functions

  void Awake() {
    name = "Voro Core";
    voroWorld = CreateChild(voroWorld);
    voroCompute = CreateChild(voroCompute);

    VoroCompute.OnCompute += HandleOnCompute;
  }

  void OnDisable() {
    VoroCompute.OnCompute -= HandleOnCompute;
  }

  #endregion

  void HandleOnCompute() {
    foreach (var chunk in voroWorld.GetAllChunks()) {
      var tex = Compute(chunk);
      chunk.SetTexture(tex);
    }

    return;

    Texture2D Compute(Chunk chunk) {
      var graph = voroCompute.Graph;
      var dictionary = ReadGraph(graph, out var allowCompute);

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
          result = effect.RunEffect(chunk);
        });
      }

      return result;
    }

    Dictionary<string, List<EffectManager>> ReadGraph(Graph graph, out bool allowCompute) {
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
          case EffectName.Slope: {
            effectList.Add(new SlopeEffectManager(node));
            break;
          }
          case EffectName.Flat: {
            effectList.Add(new FlatEffectManager(node));
            break;
          }
          case EffectName.Noise: {
            effectList.Add(new NoiseEffectManager(node));
            break;
          }
          case EffectName.Terrace: {
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

  T CreateChild<T>(T existing, string childName = "") where T : Component {
    if (existing != null) {
      return existing;
    }

    var child = new GameObject(childName);
    child.transform.SetParent(transform);
    return child.AddComponent<T>();
  }
}
}