using VoroSystem.Voro.Compute.EffectSystem.EffectDefinitions;

namespace VoroSystem.Voro.Compute.EffectSystem.Core {
public class Effect<TParam> : TypedBaseEffect<TParam>
  where TParam : class, new() {
  public Effect(EffectName name) : base(name) { }

  public override void Init() {
    switch (Name) {
    case EffectName.Slope:
      Shader = new SlopeShader<TParam>(Parameters);
      break;

    case EffectName.Noise:
    // Resources.Load<ComputeShader>("FX/Noise");
    // SetParameter<float>("Size");
    // etParameter<float>("Steepness");
    case EffectName.Flat:
    // Resources.Load<ComputeShader>("FX/Flat");
    // SetParameter<float>("Height");
    case EffectName.Terrace:
      // Resources.Load<ComputeShader>("FX/Terrace");
      // SetParameter<float>("Direction");
      // SetParameter<int>("Iterations");
      // SetParameter<float>("MinStepSize");
      // SetParameter<float>("MaxStepSize");
      // SetParameter<float>("StepSize");
      break;
    }

    Shader.SetOperationMode(Mode);
    // Debug.Log($"[{Name}] Effect initialized");
  }
}
}