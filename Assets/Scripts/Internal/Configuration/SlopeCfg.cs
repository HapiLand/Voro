using System;
using UnityEngine;

namespace Internal.Configuration {
[Serializable]
public struct SlopeCfg : IConfig {
    // this struct stores parameters for the Slope instruction
    // to set the point height as a linear gradient
    [field: SerializeField] public float[] ConfigArr { get; set; }

    public override string ToString() => $"Slope Config";
}
}