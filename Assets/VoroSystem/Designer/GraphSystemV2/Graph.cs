using System;
using System.Collections.Generic;
using UnityEngine;

namespace VoroSystem.Designer.GraphSystemV2 {
[Serializable]
public class Graph {
    #region Serialized Fields

    [SerializeField] public string graphName;
    [SerializeField] public List<Layer> layers = new();

    #endregion

    public Graph(string graphName = "Default Graph") {
        this.graphName = graphName;
    }

    public void CreateLayer(string layerName = "Default Layer") {
        var layer = new Layer(layerName);
        AddLayer(layer);
    }

    void AddLayer(Layer layer) {
        layers.Add(layer);
    }
}
}