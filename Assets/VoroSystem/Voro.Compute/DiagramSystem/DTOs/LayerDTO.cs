using System;
using System.Collections.Generic;
using Newtonsoft.Json;

// ReSharper disable InconsistentNaming

namespace VoroSystem.Voro.Compute.DiagramSystem.DTOs {
[Serializable]
public class LayerDTO {
  #region Serialized Fields

  [JsonProperty("Label")] public string name;
  [JsonProperty("Nodes")] public List<NodeDTO> nodes = new();

  #endregion

  public Layer ToLayer() {
    return Layer.CreateFromDataTransferObject(this);
  }
}
}