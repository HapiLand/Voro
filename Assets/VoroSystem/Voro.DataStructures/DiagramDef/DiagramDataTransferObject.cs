using System.Collections.Generic;
using Newtonsoft.Json;
using VoroSystem.Voro.DataStructures.LayerDef;

namespace VoroSystem.Voro.DataStructures.DiagramDef {
public class DiagramDataTransferObject {
  [JsonProperty("Name")] public string Name;
  [JsonProperty("Layers")] public List<LayerDataTransferObject> Layers;
}
}