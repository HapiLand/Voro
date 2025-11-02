using VoroSystem.World.FX.Base;
using VoroSystem.World.FX.Configuration;
using VoroSystem.World.FX.Managers.Internal;

namespace VoroSystem.World.FX.Managers {
class FlatEffectManager : EffectManager {
    public FlatEffectManager(JConfigFX jConfig) {
        _effect = (FlatEffect)MakeEffect(jConfig);
    }

    public FlatEffectManager() {
        _effect = (FlatEffect)MakeEffect();
    }

    protected override IEffect MakeEffect(JConfigFX jConfig) {
        var effect = new FlatEffect();
        effect.Initialize(jConfig);
        return effect;
    }

    protected override IEffect MakeEffect() {
        var effect = new FlatEffect();
        // effect.Initialize(config);
        return effect;
    }
}
}