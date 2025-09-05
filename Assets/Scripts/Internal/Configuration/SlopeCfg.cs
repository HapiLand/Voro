using System;
using UnityEngine;

namespace Internal.Configuration {
[Serializable]
public struct SlopeCfg : IConfiguration {
    // this struct stores parameters for the Slope instruction
    // to set the point height as a linear gradient
    [field: SerializeField] public float[] PropertiesArray { get; set; }

    public override string ToString() {
        return "Slope Config";
    }
}
}