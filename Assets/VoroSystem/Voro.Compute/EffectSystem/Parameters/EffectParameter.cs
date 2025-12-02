using System;
using UnityEngine;
using VoroSystem.Voro.Compute.EditorSystem;

namespace VoroSystem.Voro.Compute.EffectSystem.Parameters {
[Serializable]
public class EffectParameter {
    public EffectParameter(string name, object defaultValue, FieldBase.FieldType fieldType) {
        this.name = name;
        this.fieldType = fieldType;
        this.defaultValue = ConvertValue(defaultValue, fieldType);
    }

    static object ConvertValue(object value, FieldBase.FieldType type) {
        switch (type) {
        case FieldBase.FieldType.FloatField:
        case FieldBase.FieldType.Radial:
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

    #region Serialized Fields
    [SerializeReference] public object defaultValue;

    public FieldBase.FieldType fieldType;

    public string name;
    #endregion
}
}