using VoroSystem.Generation.GraphSystem.Fields;

namespace VoroSystem.Generation.DiagramSystem {
class FXField {
    public FXField(string name, object defaultValue, FieldType fieldType)
    {
        Name = name;
        FieldType = fieldType;
        DefaultValue = ConvertValue(defaultValue, fieldType);
    }


    public string Name { get; set; }
    public FieldType FieldType { get; set; }
    public object DefaultValue { get; }

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
        }

        return value;
    }
}
}