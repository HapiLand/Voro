using System.Collections.Generic;
using Newtonsoft.Json;
using VoroSystem.Voro.DataStructures.NodeDef;

namespace VoroSystem.Voro.DataStructures.LayerDef {
public class LayerDataTransferObject {
  [JsonProperty("Name")] public string Name;
  [JsonProperty("Nodes")] public List<NodeDataTransferObject> Nodes;
}
}