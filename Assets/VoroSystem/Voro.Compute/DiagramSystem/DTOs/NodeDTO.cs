using System;
using System.Collections.Generic;
using Newtonsoft.Json;

// ReSharper disable InconsistentNaming

namespace VoroSystem.Voro.Compute.DiagramSystem.DTOs {
[Serializable]
public class NodeDTO {
  #region Serialized Fields
  [JsonProperty("EffectType")] public EffectName type;
  [JsonProperty("Mode")] public OperationMode mode;
  public List<FieldDTO> fields = new();
  #endregion

  public Node ToNode() {
    return Node.CreateFromDataTransferObject(this);
  }

  public void LoadFields() {
    fields = FieldsLookup.LoadFields(type);
  }
}
}