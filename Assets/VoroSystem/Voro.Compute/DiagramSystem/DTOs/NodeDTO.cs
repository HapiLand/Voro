using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using VoroSystem.Voro.Compute.EffectSystem.Core;

// ReSharper disable InconsistentNaming

namespace VoroSystem.Voro.Compute.DiagramSystem.DTOs {
[Serializable]
public class NodeDTO {
  #region Serialized Fields

  [JsonProperty("EffectType")] public EffectBase.EffectType type;
  [JsonProperty("Mode")] public EffectBase.EffectMode mode;
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