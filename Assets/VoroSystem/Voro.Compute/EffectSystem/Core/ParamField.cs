using System;
using UnityEngine;
using VoroSystem.Voro.Compute.EditorSystem;

namespace VoroSystem.Voro.Compute.EffectSystem.Core {
[Serializable]
public class ParamField {
  #region Serialized Fields

  public FieldBase.FieldType fieldType;

  [SerializeReference] public object defaultValue;
  public string name;

  #endregion

  public ParamField(string argName, object argDefaultValue, FieldBase.FieldType argType) {
    name = argName;
    defaultValue = ConvertValue(argDefaultValue, argType);
    fieldType = argType;
  }

  object ConvertValue(object value, FieldBase.FieldType type) {
    switch (type) {
    case FieldBase.FieldType.FloatField:
    case FieldBase.FieldType.Angle:
    case FieldBase.FieldType.FloatSlider:
      if (float.TryParse(value.ToString(), out var f)) {
        return f;
      }

      break;
    case FieldBase.FieldType.Toggle:
      if (bool.TryParse(value.ToString(), out var b)) {
        return b;
      }

      break;
    case FieldBase.FieldType.IntSlider:
      if (int.TryParse(value.ToString(), out var i)) {
        return i;
      }

      break;

    default:
      throw new ArgumentOutOfRangeException(nameof(type), type, null);
    }

    return value;
  }
}
}