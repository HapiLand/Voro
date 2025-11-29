using System;
using System.Collections.Generic;
using UnityEngine;
using VoroSystem.Voro.Utilities;

namespace VoroSystem.Voro.Compute.V2.ScriptableObjects.Core {
[CreateAssetMenu(menuName = "Voro/Node/New Registry")]
[Serializable]
public class NodeRegistry : ScriptableObject {
  #region Serialized Fields

  [SerializeField] List<NodeDefinition> definitions = new();

  #endregion

  SerializableDictionary<NodeType, NodeDefinition> _lookup;

  #region Event Functions

  void OnEnable() {
    _lookup = new SerializableDictionary<NodeType, NodeDefinition>();
    foreach (var def in definitions) {
      _lookup[def.Type] = def;
    }
  }

  #endregion

  public NodeDefinition GetDefinition(NodeType type) {
    return _lookup[type];
  }
}
}