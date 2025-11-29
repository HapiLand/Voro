using System;
using UnityEngine;
using VoroSystem.Voro.Compute.V2.ScriptableObjects.Base;
using VoroSystem.Voro.Utilities;

namespace VoroSystem.Voro.Compute.V2 {
/// <summary>
/// Data that will be used as parameters to drive the Effect.
/// Replacement for FieldBase.
/// NodeData does not use Typing, interpret the NodeDataType to draw its unique design with the Editor
/// </summary>
[Serializable]
public class NodeData : NodeGUIBase {
  #region Serialized Fields

  public NodeDataDefinition dataDefinition;
  
  /// <summary> Properties that define what this NodeData controls </summary>
  public SerializableDictionary<string, object> properties;

  #endregion

  public NodeData(NodeDataDefinition definition)
  {
    dataDefinition = definition;
    properties = dataDefinition.CreateDefaultProperties();
  }

  public void DrawGUI() {
    HelperUtility.DrawUILine(Color.wheat);
    GUILayout.Label($"NodeData: \"{dataDefinition.DisplayName}\" {dataDefinition.DataType}");
    HelperUtility.DrawUILine(Color.wheat);
    dataDefinition.DrawGUI(properties);
    HelperUtility.DrawUILine(Color.wheat);
  }
}
}