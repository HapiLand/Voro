namespace VoroSystem.Generation.DiagramSystem.Effects {
class NoiseEffect : BaseEffect {
    protected override string EffectName => "Noise";

    public override void ConfigureShader() {
        base.ConfigureShader();
        SetParameter<float>("size");
        SetParameter<float>("steepness");
    }
}
}