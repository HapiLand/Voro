using VoroSystem.World.FX.Base;

namespace VoroSystem.World.FX.Managers.Internal {
class TerraceEffect : BaseEffect {
    protected override string EffectName => "Terrace";

    public override void ConfigureShader() {
        base.ConfigureShader();
        SetParameter<float>("stepsize");
        SetParameter<float>("randomness");
    }
}
}