using VoroSystem.World.FX.Base;
using VoroSystem.World.FX.Configuration;
using VoroSystem.World.FX.Managers.Internal;

namespace VoroSystem.World.FX.Managers {
class NoiseEffectManager : EffectManager {
    public NoiseEffectManager(JConfigFX jConfig) {
        _effect = (NoiseEffect)MakeEffect(jConfig);
    }

    public NoiseEffectManager() {
        _effect = (NoiseEffect)MakeEffect();
    }

    protected override IEffect MakeEffect(JConfigFX jConfig) {
        var effect = new NoiseEffect();
        effect.Initialize(jConfig);
        return effect;
    }

    protected override IEffect MakeEffect() {
        var effect = new NoiseEffect();
        // effect.Initialize(config);
        return effect;
    }
}
}