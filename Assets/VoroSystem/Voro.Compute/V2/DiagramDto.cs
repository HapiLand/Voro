using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace VoroSystem.Voro.Compute.V2 {
[Serializable]
public class DiagramDto {
  [JsonProperty("DiagramName")]
  public string diagramName;

  [JsonProperty("Layers")]
  public List<LayerDto> layers;
}
}