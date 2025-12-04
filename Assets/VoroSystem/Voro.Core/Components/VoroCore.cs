using System.Collections.Generic;
using UnityEngine;
using VoroSystem.Voro.Compute.Components;
using VoroSystem.Voro.Compute.DiagramSystem;
using VoroSystem.Voro.Compute.EffectSystem;
using VoroSystem.Voro.Compute.EffectSystem.Core;
using VoroSystem.Voro.World.ChunkStructure;
using VoroSystem.Voro.World.Components;

namespace VoroSystem.Voro.Core.Components {
[ExecuteAlways]
public class VoroCore : MonoBehaviour {
  #region Serialized Fields

  [SerializeField] VoroEvents events;
  [SerializeField] VoroWorld voroWorld;
  [SerializeField] VoroCompute voroCompute;

  #endregion

  #region Event Functions

  void Awake() {
    name = "Voro Core";
    voroWorld = CreateChild(voroWorld);
    voroCompute = CreateChild(voroCompute);

    events = VoroEvents.GetInstance();
    VoroEvents.GetInstance().OnComputeEvent += HandleOnCompute;
  }

  void OnDisable() {
    VoroEvents.GetInstance().OnComputeEvent -= HandleOnCompute;
  }

  #endregion

  void HandleOnCompute() {
    foreach (var chunk in voroWorld.GetAllChunks()) {
      var tex = Compute(chunk);
      chunk.SetTexture(tex);
    }

    return;

    Texture2D Compute(Chunk chunk) {
      var graph = voroCompute.Diagram;
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

    Dictionary<string, List<EffectManager>> ReadGraph(Diagram diagram, out bool allowCompute) {
      var layerCount = diagram.layers.Count;
      var graphDictionary = new Dictionary<string, List<EffectManager>>();

      if (layerCount == 0) {
        Debug.Log("No Layers found in Graph");
        allowCompute = false;
        return graphDictionary;
      }

      Debug.Log($"Reading {layerCount} Layers");
      for (var i = 0; i < layerCount; i++) {
        var layer = diagram.layers[i];
        var layerName = layer.name;

        // add each unique layer to the dictionary
        if (!graphDictionary.TryGetValue(layerName, out var effectList)) {
          effectList = new List<EffectManager>();
          graphDictionary[layerName] = effectList;
        }

        // read the Nodes, create the matching Effect
        foreach (var node in layer.nodes) {
          switch (node.Type) {
          case EffectBase.EffectType.Slope: {
            effectList.Add(new SlopeEffectManager(node));
            break;
          }
          case EffectBase.EffectType.Flat: {
            effectList.Add(new FlatEffectManager(node));
            break;
          }
          case EffectBase.EffectType.Noise: {
            effectList.Add(new NoiseEffectManager(node));
            break;
          }
          case EffectBase.EffectType.Terrace: {
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

  #region Compute handlers

  /*static void HandleComputeCalled(object sender, VoroComputeEvents.ComputeEventArgs e) {
    Debug.Log("[VoroCore] Compute called");
  }

  void HandleComputeBegin(object sender, VoroComputeEvents.ComputeEventArgs e) {
    Debug.Log("[VoroCore] Compute begin");
    HandleOnCompute();
  }

  static void HandleComputeComplete(object sender, VoroComputeEvents.ComputeEventArgs e) {
    Debug.Log("[VoroCore] Compute complete");
  }*/

  #endregion
}
}