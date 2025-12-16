using System.Collections.Generic;
using Newtonsoft.Json;
using VoroSystem.VoroDataStructures.ControlDef;

namespace VoroSystem.VoroDataStructures.NodeDef {
public class NodeDataTransferObject {
  [JsonProperty("Controls")] public List<ControlDataTransferObject> Controls;
  [JsonProperty("Mode")] public string Mode;
  [JsonProperty("Name")] public string Name;
}
}