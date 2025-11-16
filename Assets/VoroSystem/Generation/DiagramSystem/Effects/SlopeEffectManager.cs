using VoroSystem.Designer.GraphSystem.Graph;

namespace VoroSystem.Generation.DiagramSystem.Effects {
class SlopeEffectManager : EffectManager {
    public SlopeEffectManager(LayerEffect config) {
        Effect = (SlopeEffect)MakeEffect(config);
    }


    protected override IEffect MakeEffect(LayerEffect config) {
        var effect = new SlopeEffect();
        effect.Initialize(config);
        return effect;
    }

    protected override IEffect MakeEffect() {
        var effect = new SlopeEffect();
        effect.Initialize(null);
        return effect;
    }
}
}