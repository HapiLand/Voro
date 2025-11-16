using System;
using System.Collections.Generic;
using UnityEngine;
using VoroSystem.Designer.GraphSystemV2.Core;
using VoroSystem.Generation.DiagramSystem;

namespace VoroSystem.Designer.GraphSystemV2 {
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

    /*public static Node CreateInstance(int lookupID) {
        var names = NodeLookup.Names.ToList();
        var selected = names[lookupID];
        var def = EffectLookup.Get(selected);
        return new Node($"NodeID = {lookupID}");
    }*/
}
}