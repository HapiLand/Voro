using System;

namespace Voro.Jen.Effects {
public abstract class EffectBase {
    /// <summary>
    ///     callback to any listeners that do something when the control UI for an effect is changed
    /// </summary>
    public static Action<IEffect, object> OnControlValueChanged;

    protected void OnValueChanged(IEffect effect, object value) {
        OnControlValueChanged?.Invoke(effect, value);
    }
}
}