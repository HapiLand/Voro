using System;

namespace VoroSystem.Voro.Compute.EditorSystem {
[Serializable]
public abstract class TypedFieldBase<T> : FieldBase {
    protected TypedFieldBase(string fieldName, T defaultValue, FieldType fieldType)
        : base(fieldName, defaultValue, fieldType) { }

    public T Value {
        get => (T)defaultValue;
        set => defaultValue = value;
    }

    protected abstract IFieldDrawer<T> Drawer { get; }

    public override void DrawGUI() {
        var v = Value;
        Drawer.Draw(ref v, name);
        Value = v;
    }
}
}