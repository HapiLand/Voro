using System;
using UnityEngine;

namespace VoroSystem.Voro.Compute {
[ExecuteAlways]
public class VoroCompute : MonoBehaviour {
    public static Action OnCompute;

    #region Event Functions
    void Awake() {
        name = "Voro Compute";
    }

    void Start() {
      OnCompute?.Invoke();
    }
    #endregion

    /*void OnDesignerChanged() {
      foreach (var chunk in world.GetAllChunks()) {
        var mat = new Material(MaterialResource)
        {
          mainTexture = Compute(chunk)
        };
        GetComponent<MeshRenderer>().sharedMaterial = mat;
      }
    }*/

    /*Texture2D Compute(Chunk instance) {
      var graph = designer.graph;
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
          result = effect.RunEffect(instance);
        });
      }

      return result;
    }*/


    /*Dictionary<string, List<EffectManager>> ReadGraph(Graph graph, out bool allowCompute) {
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
    }*/
}
}