using System;
using UnityEngine;

namespace Internal.Configuration {
[Serializable]
public struct NullCfg : IConfiguration {
    [field: SerializeField] public float[] PropertiesArray { get; set; }

    public override string ToString() {
        return "Null Config";
    }
}
}