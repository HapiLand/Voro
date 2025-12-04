using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json.Linq;
using UnityEngine;
using VoroSystem.Voro.Compute.DiagramSystem.DTOs;
using VoroSystem.Voro.Compute.EditorSystem;
using VoroSystem.Voro.Compute.EditorSystem.Controls;
using VoroSystem.Voro.Compute.EffectSystem.Core;

namespace VoroSystem.Voro.Compute.DiagramSystem {
public static class FieldsLookup {
  static EffectBase.EffectType GetEffectType(JToken token) {
    var typeName = token["Type"]?.ToString();
    return Enum.TryParse(typeName, out EffectBase.EffectType type) ? type : EffectBase.EffectType.None;
  }

  public static FieldBase CreateFieldBase(FieldDTO dto) {
    switch (dto.type) {
    case FieldBase.FieldType.FloatField: {
      var value = Convert.ToSingle(dto.defaultValue);
      return new FloatField(dto.label, value);
    }
    case FieldBase.FieldType.Angle: {
      var value = Convert.ToSingle(dto.defaultValue);
      return new Angle(dto.label, value);
    }
    case FieldBase.FieldType.FloatSlider: {
      var value = Convert.ToSingle(dto.defaultValue);
      var min = Convert.ToSingle(dto.rangeMin);
      var max = Convert.ToSingle(dto.rangeMax);
      return new FloatSlider(dto.label, value, min, max);
    }
    case FieldBase.FieldType.Toggle: {
      var value = Convert.ToBoolean(dto.defaultValue);
      return new Toggle(dto.label, value);
    }
    case FieldBase.FieldType.IntSlider: {
      var value = Convert.ToInt32(dto.defaultValue);
      var min = Convert.ToInt32(dto.rangeMin);
      var max = Convert.ToInt32(dto.rangeMax);
      return new IntSlider(dto.label, value, min, max);
    }
    default:
      return null;
    }
  }

  public static List<FieldDTO> LoadFields(EffectBase.EffectType type) {
    var fields = new List<FieldDTO>();
    var root = JObject.Parse(Resources.Load<TextAsset>("Lookup").text);
    if (root["Effects"] is not JArray effectsArray) {
      return fields;
    }

    foreach (var jToken in effectsArray) {
      var effectType = GetEffectType(jToken);
      if (effectType != type) {
        continue;
      }

      if (jToken["Fields"] is not JArray fieldsArray) {
        return fields;
      }

      fields.AddRange(fieldsArray.Select(fieldToken => fieldToken.ToObject<FieldDTO>()));

      break;

      //fields.AddRange(fieldsArray.Select(fieldToken => fieldToken.ToObject<FieldDTO>()));

      /*foreach (var fieldToken in fieldsArray) {
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
      }*/
    }

    return fields;
  }
}
}