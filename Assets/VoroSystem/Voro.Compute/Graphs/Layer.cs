using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;

namespace VoroSystem.Voro.Compute.Graphs {
[Serializable]
public class Layer {
    public Layer() { }

    public Layer(string layerName) {
        this.layerName = layerName;
    }

    public void CreateNode(Node def) {
        AddNode(def);
    }

    void AddNode(Node node) {
        nodes.Add(node);
    }

    #region Serialized Fields
    [SerializeField] [JsonProperty("Name")]
    public string layerName;

    [SerializeField] [JsonProperty("Nodes")]
    public List<Node> nodes = new();
    #endregion
}
}