using Newtonsoft.Json;

namespace VoroSystem.Voro.DataStructures.ControlDef {
public class ControlDataTransferObject {
  [JsonProperty("Name")] public string Name;
  [JsonProperty("Type")] public string Type;
  [JsonProperty("Value")] public object Value;
}
}