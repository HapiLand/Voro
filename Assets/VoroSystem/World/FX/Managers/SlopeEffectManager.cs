using VoroSystem.World.FX.Base;
using VoroSystem.World.FX.Configuration;
using VoroSystem.World.FX.Managers.Internal;

namespace VoroSystem.World.FX.Managers {
class SlopeEffectManager : EffectManager {
    public SlopeEffectManager(JConfigFX jConfig) {
        _effect = (SlopeEffect)MakeEffect(jConfig);
    }

    public SlopeEffectManager() {
        _effect = (SlopeEffect)MakeEffect();
    }

    protected override IEffect MakeEffect(JConfigFX jConfig) {
        var effect = new SlopeEffect();
        effect.Initialize(jConfig);
        return effect;
    }

    protected override IEffect MakeEffect() {
        var effect = new SlopeEffect();
        effect.Initialize(null);
        return effect;
    }
}
}