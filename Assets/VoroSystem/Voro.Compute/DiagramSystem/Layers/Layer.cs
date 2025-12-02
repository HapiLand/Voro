using System;
using System.Collections.Generic;
using UnityEngine;
using VoroSystem.Voro.Compute.DiagramSystem.Nodes;
using VoroSystem.Voro.Compute.EffectSystem.Core;

namespace VoroSystem.Voro.Compute.DiagramSystem.Layers {

[Serializable]
public class Layer {
    public Layer(string name) {
        layerName = name;
    }

    public void CreateNode(INode def) {
        nodes.Add(def);
    }

    #region Serialized Fields
    [SerializeField] public string layerName;

    [SerializeField] public List<INode> nodes = new();
    #endregion
}
}