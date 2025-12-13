using System.Collections.Generic;
using Newtonsoft.Json;
using VoroSystem.Voro.DataStructures.ControlDef;

namespace VoroSystem.Voro.DataStructures.NodeDef {
public class NodeDataTransferObject {
  [JsonProperty("Controls")] public List<ControlDataTransferObject> Controls;
  [JsonProperty("Mode")] public string Mode;
  [JsonProperty("Name")] public string Name;
}
}