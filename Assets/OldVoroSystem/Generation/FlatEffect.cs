namespace OldVoroSystem.Generation {
class FlatEffect : BaseEffect {
  protected override string EffectName => "Flat";

  public override void ConfigureShader() {
    base.ConfigureShader();
    SetParameter<float>("height");
  }
}
}