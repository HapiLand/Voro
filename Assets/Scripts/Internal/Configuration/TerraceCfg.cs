using System;
using UnityEngine;

namespace Internal.Configuration {
[Serializable]
public struct TerraceCfg : IConfiguration {
    // this struct stores parameters for the Terrace instruction
    // creates a staircase effect for the point height
    [field: SerializeField] public float[] PropertiesArray { get; set; }

    public override string ToString() {
        return "Terrace Config";
    }
}
}