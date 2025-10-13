using System;
using UnityEngine;

namespace VoroSystem.GraphEditor.Effects.Parameters.Controls {
public abstract class BaseControl<T> : ITypedControl<T> {
    ITypedParam<T> _parameter;

    protected BaseControl(string name, ITypedParam<T> parameter) {
        Name = name;
        Parameter = parameter;
    }

    public string Name { get; }

    public virtual void Draw() { }

    public ITypedParam<T> Parameter {
        get => _parameter;
        set
        {
            if (Equals(_parameter, value)) {
                return;
            }

            _parameter = value;
            OnParameterChanged?.Invoke(this);
        }
    }

    public event Action<ITypedControl<T>> OnParameterChanged;

    protected void TriggerParameterChanged() {
        OnParameterChanged?.Invoke(this);
        Debug.Log($"Value changed in Parameter '{Name}' = '{Parameter.Value}'");
    }
}
}