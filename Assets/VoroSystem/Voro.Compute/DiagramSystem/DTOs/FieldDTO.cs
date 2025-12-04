using System;
using Newtonsoft.Json;
using VoroSystem.Voro.Compute.EditorSystem;

// ReSharper disable InconsistentNaming

namespace VoroSystem.Voro.Compute.DiagramSystem.DTOs {
[Serializable]
public class FieldDTO {
  #region Serialized Fields

  [JsonProperty("Type")] public FieldBase.FieldType type;
  [JsonProperty("Label")] public string label;

  #endregion

  [JsonProperty("Default")] public object defaultValue;
  [JsonProperty("RangeMax")] public object rangeMax;
  [JsonProperty("RangeMin")] public object rangeMin;

  public FieldBase ToFieldBase() {
    return FieldsLookup.CreateFieldBase(this);
  }
}
}