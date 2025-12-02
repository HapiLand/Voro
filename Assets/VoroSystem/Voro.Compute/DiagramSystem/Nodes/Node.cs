using System;
using System.Collections.Generic;
using VoroSystem.Voro.Compute.EditorSystem;
using VoroSystem.Voro.Compute.EffectSystem.Core;

namespace VoroSystem.Voro.Compute.DiagramSystem.Nodes {
[Serializable]
public class Node : INode {
    public Node(EffectBase.EffectType type, List<FieldBase> fields) {
        NodeType = type;
        Mode = EffectBase.EffectMode.Set;
        FieldData = new List<FieldData>();
        Fields = fields;
    }
    public EffectBase.EffectType NodeType { get; }
    public EffectBase.EffectMode Mode { get; set; }
    public List<FieldData> FieldData { get; set; }
    public List<FieldBase> Fields { get; set; }
}
}