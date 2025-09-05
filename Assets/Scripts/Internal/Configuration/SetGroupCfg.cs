using System;
using UnityEngine;

namespace Internal.Configuration {
[Serializable]
public struct SetGroupCfg : IConfiguration {
    [field: SerializeField] public float[] PropertiesArray { get; set; }

    public override string ToString() {
        return "Set Group Config";
    }
}
}