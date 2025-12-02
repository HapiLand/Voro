using System;
using Newtonsoft.Json;
using VoroSystem.Voro.Compute.EditorSystem;

// ReSharper disable InconsistentNaming

namespace VoroSystem.Voro.Compute.DiagramSystem.DTOs {
[Serializable]
public class FieldDTO {
    [JsonProperty("Name")] public string name;
    [JsonProperty("Type")] public string type;
    [JsonProperty("MinValue")] public float minValue;
    [JsonProperty("MaxValue")] public float maxValue;
    [JsonProperty("DefaultValue")] public object defaultValue;

    public FieldBase ToBase() {
        switch (type) {
        case "FloatField": {
            var value = Convert.ToSingle(defaultValue);
            return new FloatField(name, value);
        }
        case "FloatSlider": {
            var value = Convert.ToSingle(defaultValue);
            return new FloatSlider(name, value, minValue, maxValue);
        }
        case "IntSlider": {
            var intValue = Convert.ToInt32(defaultValue);
            return new IntSlider(name, intValue, (int)minValue, (int)maxValue);
        }
        case "Radial": {
            var value = Convert.ToSingle(defaultValue);
            return new Angle(name, value);
        }
        case "Toggle": {
            var value = Convert.ToBoolean(defaultValue);
            return new Toggle(name, value);
        }
        }

        return null;
    }
}
}