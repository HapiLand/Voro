using Voro.Core.Effects.Internal;
using Voro.Core.Effects.Internal.FX;
using Voro.Core.World;

namespace Voro.Core.Effects {
class FlatEffectManager : EffectManager {
    public FlatEffectManager(ConfigFX config) {
        _effect = (FlatEffect)MakeEffect(config);
    }

    protected override IEffect MakeEffect(ConfigFX config) {
        var effect = new FlatEffect();
        effect.Initialize(config);
        return effect;
    }
}
}