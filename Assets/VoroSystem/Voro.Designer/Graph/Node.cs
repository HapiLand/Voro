using System;
using System.Collections.Generic;
using UnityEngine;
using VoroSystem.Voro.Compute.Effects.EffectSystem.Core;
using VoroSystem.Voro.Designer.Graph.Core;

namespace VoroSystem.Voro.Designer.Graph {
[Serializable]
public class Node {
  #region Serialized Fields

  [SerializeField] public string nodeName;
  [SerializeField] public EffectOperation operation = EffectOperation.Set;
  [SerializeReference] public List<FieldBase> fields = new();

  #endregion

  public Node(string nodeName) {
    this.nodeName = nodeName;
  }
}
}