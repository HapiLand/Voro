using System.Collections.Generic;
using UnityEngine;
using VoroSystem.Voro.Compute.Effects;
using VoroSystem.Voro.Compute.Effects.Core;
using VoroSystem.Voro.Designer;
using VoroSystem.Voro.Designer.Canvas;
using VoroSystem.Voro.World;
using VoroSystem.Voro.World.TileEntities;

namespace VoroSystem.Voro.Compute {
[ExecuteAlways]
public class VoroCompute : MonoBehaviour {
  #region Serialized Fields

  [SerializeField] VoroWorld world;
  [SerializeField] VoroDesigner designer;

  #endregion

  

  #region Event Functions

  void Awake() {
    name = "Voro Compute";
    world = gameObject.AddComponent<VoroWorld>();
  }

  void OnEnable() {
    VoroDesigner.OnChanged += OnDesignerChanged;
  }

  void OnDisable() {
    VoroDesigner.OnChanged -= OnDesignerChanged;
  }

  #endregion

  public void Init(VoroDesigner designer) {
    this.designer = designer;
    designer.gameObject.transform.SetParent(transform);
  }

  Material MaterialResource => Resources.Load<Material>("ChunkMaterial");
  
  void OnDesignerChanged() {
    foreach (var entity in world.GetAllTileEntities()) {
      var mat = new Material(MaterialResource)
      {
        mainTexture = Compute(entity)
      };
      GetComponent<MeshRenderer>().sharedMaterial = mat;
    }
  }

  Texture2D Compute(TileEntity instance) {
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
}