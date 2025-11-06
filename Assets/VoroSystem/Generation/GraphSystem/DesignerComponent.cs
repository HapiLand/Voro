using System.Collections.Generic;
using UnityEngine;
using VoroSystem.Generation.DiagramSystem;
using VoroSystem.Generation.GraphSystem.Graph;

namespace VoroSystem.Generation.GraphSystem {
[ExecuteInEditMode]
public class DesignerComponent : MonoBehaviour {
    [SerializeField] public VoroGraph graph;
    int _lastGraphHash;
    public static DesignerComponent Instance { get; private set; }
    public bool HasChanged { get; set; }

    void Awake() {
        if (Instance != null) {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    void Reset() {
        graph = new VoroGraph("Example Graph");
        _lastGraphHash = graph.ComputeHash();
    }

    void Update() {
        if (graph == null) {
            return;
        }

        var currentHash = graph.ComputeHash();
        if (currentHash == _lastGraphHash) {
            return;
        }

        HasChanged = true; // mark as changed this frame
        _lastGraphHash = currentHash;
        Debug.Log("graph changed");
    }

    public void CreateLayer(string layerName) {
        graph.CreateLayer(layerName);
    }

    public void GetGraphDictionary(out Dictionary<string, List<EffectManager>> graphDictionary) {
        graphDictionary = new Dictionary<string, List<EffectManager>>();

        foreach (var layer in graph.Layers) {
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