using System;
using System.Collections.Generic;
using UnityEngine;
using VoroSystem.Compute.EffectSystem.Core;
using VoroSystem.Designer.GraphSystem.Core;

namespace VoroSystem.Designer.GraphSystem {
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