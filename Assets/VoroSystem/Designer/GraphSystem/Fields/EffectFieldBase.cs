using System;

namespace VoroSystem.Designer.GraphSystem.Fields {
[Serializable]
public abstract class EffectFieldBase {
    protected EffectFieldBase(string name, object defaultValue, FieldType fieldType) {
        Name = name;
        DefaultValue = defaultValue;
        Type = fieldType;
    }

    public string Name { get; }

    public object DefaultValue { get; set; }

    public FieldType Type { get; }

    public abstract void DrawGUI();
    public abstract int ComputeHash();
}
}