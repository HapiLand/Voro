using Voro.Core.Effects.Internal;
using Voro.Core.Effects.Internal.FX;
using Voro.Core.World;

namespace Voro.Core.Effects {
class NoiseEffectManager : EffectManager {
    public NoiseEffectManager(ConfigFX config) {
        _effect = (NoiseEffect)MakeEffect(config);
    }

    protected override IEffect MakeEffect(ConfigFX config) {
        var effect = new NoiseEffect();
        effect.Initialize(config);
        return effect;
    }
}
}