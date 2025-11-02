using VoroSystem.World.FX.Configuration;
using VoroSystem.World.Generate;

namespace VoroSystem.World.FX.Base {
public abstract class EffectManager {
    protected IEffect _effect;
    protected abstract IEffect MakeEffect(JConfigFX jConfig);
    protected abstract IEffect MakeEffect();

    public void RunEffect(BaseResult baseResult) {
        _effect.ConfigureShader();
        _effect.Compute(baseResult);
        _effect.ReadResult(baseResult);
    }
}
}