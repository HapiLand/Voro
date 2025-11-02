using System;

namespace VoroSystem.World.FX.Configuration {
class FXField {
    public FXField(JConfigField jConfigField) {
        Name = jConfigField.FieldName;
        FieldType = Enum.TryParse(jConfigField.FieldType, true, out FieldType parsed) ? parsed : FieldType.FloatField;
        DefaultValue = ConvertValue(jConfigField.DefaultValue, FieldType);
    }

    public string Name { get; set; }
    public FieldType FieldType { get; set; }
    public object DefaultValue { get; }

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
        }

        return value;
    }
}
}