using System;
using UnityEngine;

namespace Internal.Configuration {
[Serializable]
public struct NullCfg : IConfig {
    [field: SerializeField] public float[] ConfigArr { get; set; }

    public override string ToString() {
        return "Null Config";
    }
}
}