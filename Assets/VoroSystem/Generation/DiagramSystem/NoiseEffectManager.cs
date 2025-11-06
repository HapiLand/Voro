using VoroSystem.Generation.GraphSystem.Graph;

namespace VoroSystem.Generation.DiagramSystem {
class NoiseEffectManager : EffectManager {
    public NoiseEffectManager(LayerEffect config) {
        Effect = (NoiseEffect)MakeEffect(config);
    }


    protected override IEffect MakeEffect(LayerEffect config) {
        var effect = new NoiseEffect();
        effect.Initialize(config);
        return effect;
    }

    protected override IEffect MakeEffect() {
        var effect = new NoiseEffect();
        effect.Initialize(null);
        return effect;
    }
}
}