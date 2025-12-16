using System;
using System.Collections.Generic;
using Newtonsoft.Json;

// ReSharper disable InconsistentNaming

namespace VoroSystem.Voro.Compute.DiagramSystem.DTOs {
[Serializable]
public class DiagramDTO {
  #region Serialized Fields
  [JsonProperty("Label")] public string name;
  [JsonProperty("Layers")] public List<LayerDTO> layers = new();
  #endregion

  public Diagram ToDiagram() {
    return Diagram.CreateFromDataTransferObject(this);
  }
}
}