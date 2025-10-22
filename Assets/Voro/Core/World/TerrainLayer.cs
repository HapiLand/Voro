using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;
using Voro.Core.Effects;
using Voro.Core.Effects.Internal;

namespace Voro.Core.World {
class TerrainLayer {
    public TerrainLayer(ConfigLayer configLayer) {
        Name = configLayer.LayerName;
        Order = configLayer.SortOrder;

        // read the effect types in the config, create the effect managers for each
        EffectManagers = new List<EffectManager>();

        configLayer.Effects?.ForEach(config => {
            var manager = CreateEffectManager(config);
            EffectManagers.Add(manager);
        });
    }


    public string Name { get; set; }
    public int Order { get; set; }
    public List<EffectManager> EffectManagers { get; }

    EffectManager CreateEffectManager(ConfigFX config) {
        Debug.Log("[Terrain Layer] Creating Effect Manager");

        return config.EffectName switch
        {
            "Slope" => new SlopeEffectManager(config),
            "Flat" => new FlatEffectManager(config),
            "Noise" => new NoiseEffectManager(config),
            "Terrace" => new TerraceEffectManager(config),
            _ => throw new ArgumentException($"Unknown Effect Name {config.EffectName}")
        };
    }
}

struct Configuration {
    [JsonProperty("configName")] public string ConfigName { get; set; }

    [JsonProperty("layers")] public List<ConfigLayer> Layers { get; set; }
}

struct ConfigLayer {
    [JsonProperty("layerName")] public string LayerName { get; set; }

    [JsonProperty("sortOrder")] public int SortOrder { get; set; }

    [JsonProperty("effects")] public List<ConfigFX> Effects { get; set; }
}

struct ConfigField {
    [JsonProperty("fieldName")] public string FieldName { get; set; }
    [JsonProperty("fieldType")] public string FieldType { get; set; }
    [JsonProperty("defaultValue")] public object DefaultValue { get; set; }
}
}