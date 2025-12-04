using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using VoroSystem.Voro.Compute.DiagramSystem.DTOs;
using VoroSystem.Voro.Compute.EditorSystem;
using VoroSystem.Voro.Compute.EffectSystem.Core;
using VoroSystem.Voro.Utilities;

namespace VoroSystem.Voro.Compute.DiagramSystem {
[Serializable]
public class Node {
  Node(EffectBase.EffectType type, EffectBase.EffectMode mode, List<FieldBase> fields) {
    Type = type;
    Mode = mode;
    Fields = fields;
    // VoroComputeEvents.GetInstance().DiagramSystem.Node.RaiseCreated(this);
  }

  public EffectBase.EffectType Type { get; }
  public EffectBase.EffectMode Mode { get; set; }
  public List<FieldBase> Fields { get; }
  public string Name => Type.ToString();

  public static Node CreateFromDataTransferObject(NodeDTO dto) {
    var fields = dto.fields.Select(fieldDto => fieldDto.ToFieldBase()).ToList();
    return new Node(dto.type, dto.mode, fields);
  }

  public static Node CreateNewInstance(EffectBase.EffectType type) {
    return new Node(type, EffectBase.EffectMode.Set, new List<FieldBase>());
  }
}
}