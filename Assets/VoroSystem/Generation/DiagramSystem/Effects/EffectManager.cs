using VoroSystem.Designer.GraphSystem.Graph;
using VoroSystem.Generation.MesherSystem;

namespace VoroSystem.Generation.DiagramSystem.Effects {
public abstract class EffectManager {
    protected IEffect Effect;
    protected abstract IEffect MakeEffect(LayerEffect effect);
    protected abstract IEffect MakeEffect();

    public void RunEffect(BaseResult baseResult) {
        Effect.ConfigureShader();
        Effect.Compute(baseResult);
        Effect.ReadResult(baseResult);
    }
}
}