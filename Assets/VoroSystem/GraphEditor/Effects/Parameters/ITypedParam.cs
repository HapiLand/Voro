namespace VoroSystem.GraphEditor.Effects.Parameters {
public interface ITypedParam<T> : IBaseParam {
    T Value { get; set; }
}
}