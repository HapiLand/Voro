using VoroSystem.Generation.GraphSystem.Graph;

namespace VoroSystem.Generation.DiagramSystem.Effects {
class TerraceEffectManager : EffectManager {
    public TerraceEffectManager(LayerEffect config) {
        Effect = (TerraceEffect)MakeEffect(config);
    }

    protected override IEffect MakeEffect(LayerEffect config) {
        var effect = new TerraceEffect();
        effect.Initialize(config);
        return effect;
    }

    protected override IEffect MakeEffect() {
        var effect = new TerraceEffect();
        effect.Initialize(null);
        return effect;
    }
}
}