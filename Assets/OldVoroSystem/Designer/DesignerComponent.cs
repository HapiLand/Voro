using System.Collections.Generic;
using OldVoroSystem.Generation;
using UnityEngine;

namespace OldVoroSystem.Designer {
[ExecuteInEditMode]
public class DesignerComponent : MonoBehaviour {
  #region Serialized Fields

  public VoroGraph graph;

  #endregion

  int lastGraphHash;

  public static DesignerComponent Instance { get; private set; }

  public bool HasChanged { get; set; }

  #region Event Functions

  void Awake() {
    if (Instance != null) {
      Destroy(gameObject);
      return;
    }

    Instance = this;

    graph = new VoroGraph("Example Graph");
    lastGraphHash = graph.ComputeHash();
  }

  void Start() {
    CreateLayer("Starting Layer");
  }

  void Update() {
    if (graph == null) {
      return;
    }

    var currentHash = graph.ComputeHash();
    if (currentHash == lastGraphHash) {
      return;
    }

    HasChanged = true; // mark as changed this frame
    lastGraphHash = currentHash;
    Debug.Log("graph changed");
  }

  #endregion

  public void CreateLayer(string layerName) {
    graph.CreateLayer(layerName);
  }

  public void GetGraphDictionary(out Dictionary<string, List<EffectManager>> graphDictionary) {
    graphDictionary = new Dictionary<string, List<EffectManager>>();

    foreach (var layer in graph.layers) {
      var layerName = layer.Name;

      if (!graphDictionary.TryGetValue(layerName, out var effectList)) {
        effectList = new List<EffectManager>();
        graphDictionary[layerName] = effectList;
      }

      foreach (var effect in layer.Effects) {
        switch (effect.Name) {
        case "Slope": {
          var manager = new SlopeEffectManager(effect);
          effectList.Add(manager);
          break;
        }
        case "Flat": {
          var manager = new FlatEffectManager(effect);
          effectList.Add(manager);
          break;
        }
        case "Noise": {
          var manager = new NoiseEffectManager(effect);
          effectList.Add(manager);
          break;
        }
        case "Terrace": {
          var manager = new TerraceEffectManager(effect);
          effectList.Add(manager);
          break;
        }
        }
      }
    }
  }
}
}