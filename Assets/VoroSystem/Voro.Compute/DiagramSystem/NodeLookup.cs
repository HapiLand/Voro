using System;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using UnityEngine;
using VoroSystem.Voro.Compute.DiagramSystem.Nodes;
using VoroSystem.Voro.Compute.EditorSystem;
using VoroSystem.Voro.Compute.EffectSystem.Core;

namespace VoroSystem.Voro.Compute.DiagramSystem {
public static class NodeLookup {
    
    static string ReadAsset() {
        var asset = Resources.Load<TextAsset>("Lookup");
        return asset.text;
    }

    static EffectBase.EffectType GetEffectType(JToken token) {
        var typeName = token["Type"]?.ToString();
        return Enum.TryParse(typeName, out EffectBase.EffectType type) ? type : EffectBase.EffectType.None;
    }

    public static List<FieldBase> LoadEffectFields(EffectBase.EffectType type) {
        var fields = new List<FieldBase>();
        var asset = ReadAsset();
        var root = JObject.Parse(asset);

        if (root["Effects"] is not JArray effects) {
            return fields;
        }

        foreach (var effectToken in effects) {
            var effectType = GetEffectType(effectToken);
            if (effectType != type) {
                continue;
            }

            if (effectToken["Fields"] is not JArray fieldArray) {
                return fields;
            }

            foreach (var fieldToken in fieldArray) {
                var fieldType = fieldToken["Type"]?.ToString();
                var fieldName = fieldToken["Label"]?.ToString();
                var fieldDefault = fieldToken["Default"];

                switch (fieldType) {
                case "Angle": {
                    var d = fieldDefault?.ToObject<float>() ?? 0f;
                    fields.Add(new Angle(fieldName, d));
                    break;
                }
                case "FloatField": {
                    var d = fieldDefault?.ToObject<float>() ?? 0f;
                    fields.Add(new FloatField(fieldName, d));
                    break;
                }
                case "FloatSlider": {
                    var d = fieldDefault?.ToObject<float>() ?? 0f;
                    var min = fieldToken["RangeMin"]?.ToObject<float>() ?? 0f;
                    var max = fieldToken["RangeMax"]?.ToObject<float>() ?? 0f;
                    fields.Add(new FloatSlider(fieldName, d, min, max));
                    break;
                }
                case "IntSlider": {
                    var d = fieldDefault?.ToObject<int>() ?? 0;
                    var min = fieldToken["RangeMin"]?.ToObject<int>() ?? 0;
                    var max = fieldToken["RangeMax"]?.ToObject<int>() ?? 0;
                    fields.Add(new IntSlider(fieldName, d, min, max));
                    break;
                }
                case "Toggle": {
                    var d = fieldDefault?.ToObject<bool>() ?? false;
                    fields.Add(new Toggle(fieldName, d));
                    break;
                }
                }
            }

            break;
        }

        return fields;
    }

    
}
}