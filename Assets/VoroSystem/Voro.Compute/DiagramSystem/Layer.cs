using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using VoroSystem.Voro.Compute.DiagramSystem.DTOs;
using VoroSystem.Voro.Core.Events;

namespace VoroSystem.Voro.Compute.DiagramSystem {
[Serializable]
public class Layer {
  #region Serialized Fields
  [SerializeField] public string name;
  [SerializeReference] public List<Node> nodes;
  [SerializeField] EffectName selectedEnum;
  #endregion

  Layer(string name, List<Node> nodes) {
    this.name = name;
    this.nodes = nodes;
    DiagramEvents.GetInstance().OnCreateNode += CreateNode;
    DiagramEvents.GetInstance().OnMoveNode += MoveNode;
    DiagramEvents.GetInstance().OnRemoveNode += RemoveNode;
  }

  void MoveNode(Node node, int direction) {
    var index = nodes.IndexOf(node);
    if (index == -1) {
      return;
    }

    var newIndex = index + direction;
    if (newIndex < 0 || newIndex >= nodes.Count) {
      return;
    }

    nodes.RemoveAt(index);
    nodes.Insert(newIndex, node);
  }

  void RemoveNode(Node node) {
    nodes.Remove(node);
  }

  public static Layer CreateFromDataTransferObject(LayerDTO dto) {
    var nodes = dto.nodes.Select(nodeDto => nodeDto.ToNode()).ToList();
    return new Layer(dto.name, nodes);
  }

  public static Layer CreateNewInstance(string layerName) {
    return new Layer(layerName, new List<Node>());
  }


  void CreateNode(EffectName effectType) {
    var node = Node.CreateNewInstance(effectType);
    nodes.Add(node);
  }
}
}