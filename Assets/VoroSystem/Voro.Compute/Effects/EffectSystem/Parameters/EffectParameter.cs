using System;
using UnityEngine;
using VoroSystem.Voro.Designer.Canvas.Core;

namespace VoroSystem.Voro.Compute.Effects.EffectSystem.Parameters {
[Serializable]
public class EffectParameter {
    public EffectParameter(string name, object defaultValue, FieldType fieldType) {
        this.name = name;
        this.fieldType = fieldType;
        this.defaultValue = ConvertValue(defaultValue, fieldType);
    }

    static object ConvertValue(object value, FieldType type) {
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

    #region Serialized Fields
    [SerializeReference] public object defaultValue;

    public FieldType fieldType;

    public string name;
    #endregion
}
}