using System.Collections.Generic;
using OldVoroSystem.Generation;
using UnityEngine;

namespace OldVoroSystem.Designer {
[ExecuteAlways]
public class GraphComponent : MonoBehaviour {
  #region Serialized Fields

  [SerializeField] int lastGraphHash;

  [SerializeField] VoroGraph _graph;

  #endregion

  public bool HasChanged { get; set; }

  #region Event Functions

  void Awake() {
    _graph = new VoroGraph("Example Graph");
    lastGraphHash = _graph.ComputeHash();
    CreateLayer("Starting Layer");
  }

  void Update() {
    if (_graph == null) {
      return;
    }

    var currentHash = _graph.ComputeHash();
    if (currentHash == lastGraphHash) {
      return;
    }

    HasChanged = true; // mark as changed this frame
    lastGraphHash = currentHash;
    Debug.Log("graph changed");
  }

  #endregion

  public void CreateLayer(string layerName) {
    _graph.CreateLayer(layerName);
  }

  public void GetGraphDictionary(out Dictionary<string, List<EffectManager>> graphDictionary) {
    graphDictionary = new Dictionary<string, List<EffectManager>>();

    foreach (var layer in _graph.layers) {
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