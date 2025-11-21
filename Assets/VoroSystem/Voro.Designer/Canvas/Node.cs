using System;
using System.Collections.Generic;
using UnityEngine;
using VoroSystem.Voro.Compute.Effects.EffectSystem.Core;
using VoroSystem.Voro.Designer.Canvas.Core;

namespace VoroSystem.Voro.Designer.Canvas {
[Serializable]
public class Node : INode {
    public Node(string nodeName) {
        this.nodeName = nodeName;
    }

    #region Serialized Fields
    [SerializeField] public string nodeName;
    [SerializeField] public EffectOperation operation = EffectOperation.Set;
    [SerializeReference] public List<FieldBase> fields = new();
    #endregion
}
}