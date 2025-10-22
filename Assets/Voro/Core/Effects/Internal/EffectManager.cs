using Voro.Core.World;

namespace Voro.Core.Effects.Internal {
abstract class EffectManager {
    protected IEffect _effect;
    protected abstract IEffect MakeEffect(ConfigFX config);

    public void RunEffect(BaseResult baseResult) {
        _effect.ConfigureShader();
        _effect.Compute(baseResult);
        _effect.ReadResult(baseResult);
    }
}
}