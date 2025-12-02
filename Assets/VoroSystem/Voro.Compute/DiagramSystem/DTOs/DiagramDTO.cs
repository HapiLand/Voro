using System;
using System.Collections.Generic;
using Newtonsoft.Json;

// ReSharper disable InconsistentNaming

namespace VoroSystem.Voro.Compute.DiagramSystem.DTOs {
[Serializable]
public class DiagramDTO {
    [JsonProperty("Name")] public string name;
    [JsonProperty("Layers")] public List<LayerDTO> layers = new();

    public DiagramDTO(string name) {
        this.name = name;
    }

    public Diagram ToDiagram() {
        var graph = new Diagram(name);
        foreach (var layerDto in layers) {
            graph.layers.Add(layerDto.ToLayer());
        }

        return graph;
    }
}
}