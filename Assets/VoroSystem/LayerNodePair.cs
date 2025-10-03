using System;
using System.Collections.Generic;

namespace VoroSystem {
[Serializable]
public class LayerNodePair {
    public string Layer;
    public List<string> Nodes = new();
    [NonSerialized] public bool FoldoutState; // store foldout state

    public LayerNodePair(string layerName) {
        Layer = layerName;
    }
}
}