using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace VoroSystem.Generation.GraphSystem.Graph {
[Serializable]
public class VoroGraph {
    public VoroGraph(string name) {
        Name = name;
    }

    public string Name { get; }
    public List<GraphLayer> Layers { get; } = new();

    public void CreateLayer(string name) {
        Debug.Log("Adding a new Layer to the Graph");
        Layers.Add(new GraphLayer(name));
    }
    
    /// <summary>
    /// detect if anything in the graph changes
    /// </summary>
    public int ComputeHash() {
        var hash = Name.GetHashCode();
        return Layers.Aggregate(hash, (current, layer) => current * 31 + layer.ComputeHash());
    }
}
}