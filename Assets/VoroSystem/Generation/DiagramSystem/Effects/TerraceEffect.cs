namespace VoroSystem.Generation.DiagramSystem.Effects {
class TerraceEffect : BaseEffect {
    protected override string EffectName => "Terrace";

    public override void ConfigureShader() {
        base.ConfigureShader();
        SetParameter<float>("direction");
        SetParameter<int>("iterations");
        SetParameter<float>("min_step_size");
        SetParameter<float>("max_step_size");
        SetParameter<float>("stepsize");
    }
}
}