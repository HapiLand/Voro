using VoroSystem.World.FX.Base;

namespace VoroSystem.World.FX.Managers.Internal {
class SlopeEffect : BaseEffect {
    protected override string EffectName => "Slope";

    public override void ConfigureShader() {
        base.ConfigureShader();
        SetParameter<float>("direction");
        SetParameter<float>("steepness");
        SetParameter<bool>("reverse");
    }
}
}