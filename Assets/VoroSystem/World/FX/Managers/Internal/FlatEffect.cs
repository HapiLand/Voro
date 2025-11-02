using VoroSystem.World.FX.Base;

namespace VoroSystem.World.FX.Managers.Internal {
class FlatEffect : BaseEffect {
    protected override string EffectName => "Flat";

    public override void ConfigureShader() {
        base.ConfigureShader();
        SetParameter<float>("height");
    }
}
}