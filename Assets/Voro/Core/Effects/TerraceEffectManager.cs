using Voro.Core.Effects.Internal;
using Voro.Core.Effects.Internal.FX;
using Voro.Core.World;

namespace Voro.Core.Effects {
class TerraceEffectManager : EffectManager {
    public TerraceEffectManager(ConfigFX config) {
        _effect = (TerraceEffect)MakeEffect(config);
    }

    protected override IEffect MakeEffect(ConfigFX config) {
        var effect = new TerraceEffect();
        effect.Initialize(config);
        return effect;
    }
}
}