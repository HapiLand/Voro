using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using VoroSystem.Voro.Compute.DiagramSystem.DTOs;
using VoroSystem.Voro.Compute.EffectSystem.Core;
using VoroSystem.Voro.Core;
using VoroSystem.Voro.Utilities;

namespace VoroSystem.Voro.Compute.DiagramSystem {
[Serializable]
public class Layer {
  #region Serialized Fields

  [SerializeField] public string name;
  [SerializeReference] public List<Node> nodes;
  [SerializeField] EffectBase.EffectType selectedEnum;
  [SerializeField] VoroEvents events;

  #endregion

  Layer(string name, List<Node> nodes) {
    this.name = name;
    this.nodes = nodes;
    // VoroComputeEvents.GetInstance().DiagramSystem.Layer.RaiseCreated(this);
    events = VoroEvents.GetInstance();
    events.OnNewNodeEvent += CreateNode;
  }

  public static Layer CreateFromDataTransferObject(LayerDTO dto) {
    var nodes = dto.nodes.Select(nodeDto => nodeDto.ToNode()).ToList();
    return new Layer(dto.name, nodes);
  }

  public static Layer CreateNewInstance(string layerName) {
    return new Layer(layerName, new List<Node>());
  }


  void CreateNode(EffectBase.EffectType effectType) {
    var node = Node.CreateNewInstance(effectType);
    nodes.Add(node);
  }
}
}