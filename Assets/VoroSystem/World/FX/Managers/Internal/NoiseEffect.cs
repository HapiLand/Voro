using VoroSystem.World.FX.Base;

namespace VoroSystem.World.FX.Managers.Internal {
class NoiseEffect : BaseEffect {
    protected override string EffectName => "Noise";

    public override void ConfigureShader() {
        base.ConfigureShader();
        SetParameter<float>("size");
        SetParameter<float>("steepness");
    }
}
}