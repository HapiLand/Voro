using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;
using VoroSystem.Voro.Compute.Effects.Core;
using VoroSystem.Voro.Designer.Canvas.Core;
using VoroSystem.Voro.Designer.Canvas.Fields;

namespace VoroSystem.Voro.Designer.Canvas {
[Serializable]
public class Node {
    public Node() { }

    public Node(EffectName nodeName) {
        this.nodeName = nodeName;
    }

    public void ConvertFields() {
        fields.Clear();
        foreach (var data in fieldData) {
            switch (data.type) {
            case "FloatField": {
                var value = Convert.ToSingle(data.defaultValue);
                fields.Add(new FloatField(data.name, value));
                break;
            }
            case "FloatSlider": {
                var value = Convert.ToSingle(data.defaultValue);
                fields.Add(new FloatSlider(data.name, value, data.minValue, data.maxValue));
                break;
            }
            case "IntSlider": {
                var intValue = Convert.ToInt32(data.defaultValue);
                fields.Add(new IntSlider(data.name, intValue, (int)data.minValue, (int)data.maxValue));
                break;
            }
            case "Radial": {
                var value = Convert.ToSingle(data.defaultValue);
                fields.Add(new Radial(data.name, value));
                break;
            }
            case "Toggle": {
                var value = Convert.ToBoolean(data.defaultValue);
                fields.Add(new Toggle(data.name, value));
                break;
            }
            default:
                throw new Exception($"Unknown field type: {data.type}");
            }
        }
    }

    #region Serialized Fields
    [SerializeField] [JsonProperty("Name")]
    public EffectName nodeName;

    [SerializeField] [JsonProperty("Operation")]
    public EffectOperation operation = EffectOperation.Set;

    [SerializeReference] [JsonProperty("Fields")]
    public List<FieldData> fieldData = new();

    [SerializeReference] public List<FieldBase> fields = new();
    #endregion
}
}