using System.Collections.Generic;
using Newtonsoft.Json;

namespace Voro.Core.World {
struct ConfigFX {
    [JsonProperty("effectName")] public string EffectName { get; set; }

    [JsonProperty("operation")] public string Operation { get; set; }

    [JsonProperty("fields")] public List<ConfigField> Fields { get; set; }
}
}