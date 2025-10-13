namespace VoroSystem.GraphEditor.Effects.Parameters {
public abstract class BaseParam<T> : ITypedParam<T> {
    protected BaseParam(string name, T value) {
        Name = name;
        Value = value;
    }

    public string Name { get; }
    public T Value { get; set; }
}
}