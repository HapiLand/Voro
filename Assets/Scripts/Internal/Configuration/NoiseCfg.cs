using System;
using UnityEngine;

namespace Internal.Configuration {
[Serializable]
public struct NoiseCfg : IConfig {
    // this struct stores parameters for the Noise instruction
    // to control how noise is combined into the height of the points
    [field: SerializeField] public float[] ConfigArr { get; set; }
    public override string ToString() => $"Noise Config";
}
}