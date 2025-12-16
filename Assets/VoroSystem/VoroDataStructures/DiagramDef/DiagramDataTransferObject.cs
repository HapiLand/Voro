using System.Collections.Generic;
using Newtonsoft.Json;
using VoroSystem.VoroDataStructures.LayerDef;

namespace VoroSystem.VoroDataStructures.DiagramDef {
public class DiagramDataTransferObject {
  [JsonProperty("Layers")] public List<LayerDataTransferObject> Layers;
  [JsonProperty("Name")] public string Name;
}
}