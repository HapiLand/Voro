using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;

namespace VoroSystem.Voro.Compute.Graphs {
[Serializable]
public class Graph {
    public Graph() { }

    public Graph(string graphName = "Default Graph") {
        this.graphName = graphName;
    }

    public Layer CreateLayer(string layerName = "Default Layer") {
        var layer = new Layer(layerName);
        AddLayer(layer);
        return layer;
    }

    void AddLayer(Layer layer) {
        layers.Add(layer);
    }

    #region Serialized Fields
    [SerializeField] [JsonProperty("Name")]
    public string graphName;

    [SerializeField] [JsonProperty("Layers")]
    public List<Layer> layers = new();
    #endregion
}
}