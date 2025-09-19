using System;

namespace OLD.VoroEditor.Effects.Internal {
public abstract class EffectBase {
    public static Action<IEffect> OnAnyEffectChanged;

    protected void NotifyDataChanged(IEffect effect) {
        OnAnyEffectChanged?.Invoke(effect);
        // Debug.Log($"EffectBase.Invoke success");
    }
}
}