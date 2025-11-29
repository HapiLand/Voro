using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace VoroSystem.Voro.Compute.V2 {
[Serializable]
public class LayerDto {
  [JsonProperty("LayerName")]
  public string layerName;

  [JsonProperty("Nodes")]
  public List<NodeDto> nodes;
}
}