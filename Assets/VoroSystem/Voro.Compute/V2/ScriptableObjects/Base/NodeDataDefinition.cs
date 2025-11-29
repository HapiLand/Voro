using System;
using UnityEngine;
using VoroSystem.Voro.Utilities;

namespace VoroSystem.Voro.Compute.V2.ScriptableObjects.Base {
/// <summary>
/// Base definition for NodeData objects
/// </summary>    
[Serializable]
public abstract class NodeDataDefinition : ScriptableObject {
  #region Serialized Fields

  [SerializeField] public string displayName;
  [SerializeField] public DataType dataType;

  #endregion

  protected const int LabelWidth = 150;
  protected const int ValueWidth = 100;
  protected const int InputWidth = 80;
  public event Action<SerializableDictionary<string, object>> OnValueChanged;
  protected void NotifyChange(SerializableDictionary<string, object> properties) {
    OnValueChanged?.Invoke(properties);
  }
  public string DisplayName => displayName;
  public DataType DataType {
    get => dataType;
    set => dataType = value;
  }

  public abstract SerializableDictionary<string, object> CreateDefaultProperties();
  public abstract void DrawGUI(SerializableDictionary<string, object> properties);
  public abstract void ResetToDefaults();
}
}