using VoroSystem.Generation.GraphSystem.Graph;

namespace VoroSystem.Generation.DiagramSystem.Effects {
class FlatEffectManager : EffectManager {
    public FlatEffectManager(LayerEffect config) {
        Effect = (FlatEffect)MakeEffect(config);
    }


    protected override IEffect MakeEffect(LayerEffect config) {
        var effect = new FlatEffect();
        effect.Initialize(config);
        return effect;
    }

    protected override IEffect MakeEffect() {
        var effect = new FlatEffect();
        effect.Initialize(null);
        return effect;
    }
}
}