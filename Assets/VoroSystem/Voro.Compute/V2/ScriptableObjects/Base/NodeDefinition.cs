using System;
using UnityEngine;

namespace VoroSystem.Voro.Compute.V2.ScriptableObjects.Base {
/// <summary>
/// Definition for a Node
/// </summary>
[CreateAssetMenu(menuName = "Voro/Node/New Definition")]
[Serializable]
public class NodeDefinition : ScriptableObject {
  #region Serialized Fields

  [SerializeField] NodeType type;
  [SerializeField] NodeDataDefinition[] dataDefinitions;

  #endregion

  public NodeType Type => type;
  public NodeDataDefinition[] DataDefinitions => dataDefinitions;
}
}