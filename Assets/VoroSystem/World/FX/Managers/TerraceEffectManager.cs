using VoroSystem.World.FX.Base;
using VoroSystem.World.FX.Configuration;
using VoroSystem.World.FX.Managers.Internal;

namespace VoroSystem.World.FX.Managers {
class TerraceEffectManager : EffectManager {
    public TerraceEffectManager(JConfigFX jConfig) {
        _effect = (TerraceEffect)MakeEffect(jConfig);
    }

    public TerraceEffectManager() {
        _effect = (TerraceEffect)MakeEffect();
    }

    protected override IEffect MakeEffect(JConfigFX jConfig) {
        var effect = new TerraceEffect();
        effect.Initialize(jConfig);
        return effect;
    }

    protected override IEffect MakeEffect() {
        var effect = new TerraceEffect();
        // effect.Initialize(config);
        return effect;
    }
}
}