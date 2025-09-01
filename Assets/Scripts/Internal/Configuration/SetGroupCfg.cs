using System;
using UnityEngine;

namespace Internal.Configuration {
[Serializable]
public struct SetGroupCfg : IConfig {
    [field: SerializeField] public float[] ConfigArr { get; set; }

    public override string ToString() {
        return "Set Group Config";
    }
}
}