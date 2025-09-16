using System;

namespace EditorGUI.Source.Effects.Base {
/// <summary>
///     base definition for the Effect class
/// </summary>
public abstract class EffectBase {
    public static Action<IEffect> OnEffectChanged;

    /// <summary>
    ///     called when any property of the Effect has been changed
    /// </summary>
    /// <param name="effect"></param>
    protected void NotifyOnChange(IEffect effect) {
        OnEffectChanged?.Invoke(effect);
    }
}
}