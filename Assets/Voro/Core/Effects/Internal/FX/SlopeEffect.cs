namespace Voro.Core.Effects.Internal.FX {
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