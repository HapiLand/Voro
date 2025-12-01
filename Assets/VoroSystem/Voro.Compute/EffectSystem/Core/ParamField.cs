using System;
using UnityEngine;
using VoroSystem.Voro.Compute.EditorSystem;

namespace VoroSystem.Voro.Compute.EffectSystem.Core {
[Serializable]
public class ParamField {
  #region Serialized Fields

  public FieldType fieldType;

  [SerializeReference] public object defaultValue;
  public string name;

  #endregion

  public ParamField(string argName, object argDefaultValue, FieldType argType) {
    name = argName;
    defaultValue = ConvertValue(argDefaultValue, argType);
    fieldType = argType;
  }

  object ConvertValue(object value, FieldType type) {
    switch (type) {
    case FieldType.FloatField:
    case FieldType.Radial:
    case FieldType.FloatSlider:
      if (float.TryParse(value.ToString(), out var f)) {
        return f;
      }

      break;
    case FieldType.Toggle:
      if (bool.TryParse(value.ToString(), out var b)) {
        return b;
      }

      break;
    case FieldType.IntSlider:
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