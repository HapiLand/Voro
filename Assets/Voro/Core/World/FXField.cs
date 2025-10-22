using System;

namespace Voro.Core.World {
class FXField {
    public FXField(ConfigField configField) {
        Name = configField.FieldName;
        FieldType = Enum.TryParse(configField.FieldType, true, out FieldType parsed) ? parsed : FieldType.FloatField;
        DefaultValue = ConvertValue(configField.DefaultValue, FieldType);
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