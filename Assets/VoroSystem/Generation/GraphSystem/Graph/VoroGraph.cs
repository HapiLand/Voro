using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace VoroSystem.Generation.GraphSystem.Graph {
public class VoroGraph {
    public List<GraphLayer> layers = new();
    public string name;

    public VoroGraph(string name) {
        this.name = name;
    }

    public void CreateLayer(string layerName) {
        Debug.Log("Adding a new Layer to the Graph");
        layers.Add(new GraphLayer(layerName));
    }

    /// <summary>
    /// detect if anything in the graph changes
    /// </summary>
    public int ComputeHash() {
        var hash = name.GetHashCode();
        return layers.Aggregate(hash, (current, layer) => current * 31 + layer.ComputeHash());
    }
}
}