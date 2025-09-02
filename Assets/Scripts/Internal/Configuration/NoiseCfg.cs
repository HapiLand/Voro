using System;
using UnityEngine;

namespace Internal.Configuration {
[Serializable]
public struct NoiseCfg : IConfiguration {
    // this struct stores parameters for the Noise instruction
    // to control how noise is combined into the height of the points
    [field: SerializeField] public float[] PropertiesArray { get; set; }

    public override string ToString() {
        return "Noise Config";
    }
}
}