using Newtonsoft.Json;

namespace VoroSystem.World.FX.Configuration {
public struct JConfigField {
    [JsonProperty("fieldName")] public string FieldName { get; set; }
    [JsonProperty("fieldType")] public string FieldType { get; set; }
    [JsonProperty("defaultValue")] public object DefaultValue { get; set; }
}
}