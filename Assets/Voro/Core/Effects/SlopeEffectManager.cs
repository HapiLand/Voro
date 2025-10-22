using Voro.Core.Effects.Internal;
using Voro.Core.Effects.Internal.FX;
using Voro.Core.World;

namespace Voro.Core.Effects {
class SlopeEffectManager : EffectManager {
    public SlopeEffectManager(ConfigFX config) {
        _effect = (SlopeEffect)MakeEffect(config);
    }

    protected override IEffect MakeEffect(ConfigFX config) {
        var effect = new SlopeEffect();
        effect.Initialize(config);
        return effect;
    }
}
}