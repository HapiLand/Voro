using System;
using UnityEngine;

namespace Internal.Configuration {
[Serializable]
public struct TerraceCfg : IConfig {
    // this struct stores parameters for the Terrace instruction
    // creates a staircase effect for the point height
    [field: SerializeField] public float[] ConfigArr { get; set; }
}
}