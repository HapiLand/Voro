using System;
using System.Collections.Generic;
using UnityEngine;
using VoroSystem.Voro.Compute.DiagramSystem.Layers;

namespace VoroSystem.Voro.Compute.DiagramSystem {
[Serializable]
public class Diagram {
    public Diagram(string name) {
        diagramName = name;
    }

    public Layer CreateLayer(string layerName) {
        var layer = new Layer(layerName);
        layers.Add(layer);
        return layer;
    }

    #region Serialized Fields
    [SerializeField] public string diagramName;
    [SerializeField] public List<Layer> layers = new();
    #endregion
}
}