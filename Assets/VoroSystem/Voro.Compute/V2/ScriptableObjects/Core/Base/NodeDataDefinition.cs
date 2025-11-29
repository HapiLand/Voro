using System;
using UnityEngine;
using VoroSystem.Voro.Utilities;

namespace VoroSystem.Voro.Compute.V2.ScriptableObjects.Core.Base {
/// <summary>
/// Base definition for NodeData objects
/// </summary>    
[Serializable]
public abstract class NodeDataDefinition : ScriptableObject {
  #region Serialized Fields

  [SerializeField] string displayName;
  [SerializeField] DataType dataType;

  #endregion

  protected const int LabelWidth = 150;
  protected const int ValueWidth = 100;
  protected const int InputWidth = 80;
  
  public string DisplayName => displayName;
  public DataType DataType => dataType;
  public abstract SerializableDictionary<string, object> CreateDefaultProperties();
  public abstract void DrawGUI(SerializableDictionary<string, object> properties);
  public abstract void ResetToDefaults();
}
}