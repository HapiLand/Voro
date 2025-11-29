using System;
using Newtonsoft.Json;
using UnityEngine;

namespace VoroSystem.Voro.Compute.Graphs.Core {
[Serializable]
public struct FieldData {
    [JsonProperty("Name")] public string name;

    [JsonProperty("DefaultValue")] [SerializeReference]
    public object defaultValue;

    [JsonProperty("Type")] public string type;

    [JsonProperty("MinValue")] public float minValue;

    [JsonProperty("MaxValue")] public float maxValue;
}
}