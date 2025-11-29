using System;
using UnityEngine;
using VoroSystem.Voro.Compute.V2.ScriptableObjects.Core.Base;
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
  
  /*/// <summary> Name of this collection of parameters </summary>
  public string dataName;

  /// <summary>
  /// controls what properties this NodeData relates too.
  /// SliderFloat has a min/max range.
  /// Toggle is a bool.
  /// </summary>
  public DataType dataType;*/

  /// <summary> Properties that define what this NodeData controls </summary>
  public SerializableDictionary<string, object> properties;

  #endregion

  public NodeData(NodeDataDefinition definition)
  {
    dataDefinition = definition;
    properties = dataDefinition.CreateDefaultProperties();
  }
  
  /*public NodeData(string dataName, DataType dataType) {
    this.dataName = dataName;
    this.dataType = dataType;
    properties = PropertyFactory.Create(dataType);
  }*/

  public void DrawGUI() {
    HelperUtility.DrawUILine(Color.wheat);
    GUILayout.Label($"NodeData: \"{dataDefinition.DisplayName}\" {dataDefinition.DataType}");
    HelperUtility.DrawUILine(Color.wheat);
    dataDefinition.DrawGUI(properties);
    HelperUtility.DrawUILine(Color.wheat);
    
    /*HelperUtility.DrawUILine(Color.wheat);
    GUILayout.Label($"NodeData: \"{dataName}\" {dataType}");
    HelperUtility.DrawUILine(Color.wheat);
    switch (dataType) {
    case DataType.SliderFloat: {
      var min = (float)properties["Min"];
      var max = (float)properties["Max"];
      var current = (float)properties["CurrentValue"];
      var updated = GUILayout.HorizontalSlider(current, min, max);
      properties["CurrentValue"] = updated;
      GUILayout.Label(updated.ToString());
      break;
    }

    case DataType.Toggle: {
      var current = (bool)properties["CurrentValue"];
      var updated = GUILayout.Toggle(current, dataName);
      properties["CurrentValue"] = updated;
      break;
    }

    case DataType.InputText: {
      var current = (string)properties["CurrentValue"];
      var updated = GUILayout.TextField(current);
      properties["CurrentValue"] = updated;
      break;
    }

    default: {
      GUILayout.Label("GUI not implemented for this type");
      break;
    }
    }

    HelperUtility.DrawUILine(Color.wheat);*/
  }
}
}