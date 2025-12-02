using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using VoroSystem.Voro.Compute.DiagramSystem.Layers;

// ReSharper disable InconsistentNaming

namespace VoroSystem.Voro.Compute.DiagramSystem.DTOs {
[Serializable]
public class LayerDTO {
    [JsonProperty("Name")] public string name;
    [JsonProperty("Nodes")] public List<NodeDTO> nodes = new();

    public Layer ToLayer() {
        var layer = new Layer(name);
        foreach (var nodeDto in nodes) {
            layer.nodes.Add(nodeDto.ToNode());
        }

        return layer;
    }
}
}