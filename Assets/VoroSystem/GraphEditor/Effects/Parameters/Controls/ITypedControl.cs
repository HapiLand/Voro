using System;

namespace VoroSystem.GraphEditor.Effects.Parameters.Controls {
public interface ITypedControl<T> : IBaseControl {
    ITypedParam<T> Parameter { get; set; }
    event Action<ITypedControl<T>> OnParameterChanged;
}
}