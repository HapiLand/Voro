using System.Collections.Generic;
using UnityEngine;

namespace Voro.UI {
public class GenerationLayers {
    public Dictionary<LayerInfo, List<NodeInfo>> LayerDictionary = new();

    public GenerationLayers() {
        Debug.Log("New GenerationLayers");
    }
}
}