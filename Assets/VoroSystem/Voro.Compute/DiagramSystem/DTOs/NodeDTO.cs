using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using VoroSystem.Voro.Compute.DiagramSystem.Nodes;
using VoroSystem.Voro.Compute.EffectSystem.Core;

// ReSharper disable InconsistentNaming

namespace VoroSystem.Voro.Compute.DiagramSystem.DTOs {
[Serializable]
public class NodeDTO {
    [JsonProperty("Name")] public EffectBase.EffectType type;
    [JsonProperty("Operation")] public EffectBase.EffectMode mode;
    [JsonProperty("Fields")] public List<FieldDTO> fields = new();

    public INode ToNode() {
        var node = NodeFactory.Create(type);
        foreach (var fieldDto in fields) {
            node.Fields.Add(fieldDto.ToBase());
        }

        return node;
    }
}
}