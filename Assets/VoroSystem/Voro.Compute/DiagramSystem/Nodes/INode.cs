using System.Collections.Generic;
using VoroSystem.Voro.Compute.EditorSystem;
using VoroSystem.Voro.Compute.EffectSystem.Core;

namespace VoroSystem.Voro.Compute.DiagramSystem.Nodes {
public interface INode {
    EffectBase.EffectType NodeType { get; }
    EffectBase.EffectMode Mode { get; set; }
    List<FieldData> FieldData { get; set; }
     List<FieldBase> Fields { get; set; }
}
}