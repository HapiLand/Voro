using System;
using UnityEngine;
using VoroSystem.Voro.Compute.V2.ScriptableObjects.Core.Base;

namespace VoroSystem.Voro.Compute.V2.ScriptableObjects.Core {
/// <summary>
/// Definition for a Node
/// </summary>
[CreateAssetMenu(menuName = "Voro/Node/New Definition")]
[Serializable]
public class NodeDefinition : ScriptableObject
{
  [SerializeField] NodeType type;
  [SerializeField] NodeDataDefinition[] dataDefinitions;

  public NodeType Type => type;
  public NodeDataDefinition[] DataDefinitions => dataDefinitions;
}
}