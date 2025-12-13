using System;
using Newtonsoft.Json;
using UnityEngine;
using VoroSystem.Voro.Compute.EditorSystem;

// ReSharper disable InconsistentNaming

namespace VoroSystem.Voro.Compute.DiagramSystem.DTOs {
[Serializable]
public class FieldDTO {
  #region Serialized Fields

  [JsonProperty("Type")] public ControlType type;
  [JsonProperty("Label")] public string label;

  [JsonProperty("Default")] [SerializeReference]
  public object defaultValue;

  [JsonProperty("RangeMax")] [SerializeReference]
  public object rangeMax;

  [JsonProperty("RangeMin")] [SerializeReference]
  public object rangeMin;

  #endregion

  public ControlBase ToFieldBase() {
    return FieldsLookup.CreateFieldBase(this);
  }
}
}