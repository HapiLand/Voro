using System;
using Newtonsoft.Json;

namespace VoroSystem.Voro.Compute.V2 {
[Serializable]
public class NodeDto {
  [JsonProperty("NodeType")]
  public NodeType nodeType;

  [JsonProperty("Mode")]
  public OperationMode mode;
}
}