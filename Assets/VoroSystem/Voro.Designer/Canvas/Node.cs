using System;
using System.Collections.Generic;
using UnityEngine;
using VoroSystem.Voro.Compute.Effects.Core;
using VoroSystem.Voro.Designer.Canvas.Core;

namespace VoroSystem.Voro.Designer.Canvas {
[Serializable]
public class Node {
  #region Serialized Fields

  [SerializeField] public EffectName nodeName;
  [SerializeField] public EffectOperation operation = EffectOperation.Set;
  [SerializeReference] public List<FieldBase> fields = new();

  #endregion

  public Node(EffectName nodeName) {
    this.nodeName = nodeName;
  }
}
}