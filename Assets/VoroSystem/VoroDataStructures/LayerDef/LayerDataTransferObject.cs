using System.Collections.Generic;
using Newtonsoft.Json;
using VoroSystem.VoroDataStructures.NodeDef;

namespace VoroSystem.VoroDataStructures.LayerDef {
public class LayerDataTransferObject {
  [JsonProperty("Name")] public string Name;
  [JsonProperty("Nodes")] public List<NodeDataTransferObject> Nodes;
}
}