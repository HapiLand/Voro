using System.Collections.Generic;
using Newtonsoft.Json;

namespace VoroSystem.World.FX.Configuration {
public struct JConfigFX {
    [JsonProperty("effectName")] public string EffectName { get; set; }

    [JsonProperty("operation")] public string Operation { get; set; }

    [JsonProperty("fields")] public List<JConfigField> Fields { get; set; }
}
}