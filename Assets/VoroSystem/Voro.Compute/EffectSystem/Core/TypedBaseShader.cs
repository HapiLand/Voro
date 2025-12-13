namespace VoroSystem.Voro.Compute.EffectSystem.Core {
public abstract class TypedBaseShader<TParam> : BaseShader where TParam : class {
  protected TypedBaseShader(TParam parameters) {
    Parameters = parameters;
    // Debug.Log($"[BaseShader<{typeof(TParam).Name}>] Created");
  }

  public TParam Parameters { get; }


  protected void SetOperationMode() {
    var prop = Parameters.GetType().GetProperty("Mode");
    if (prop != null) {
      var value = (OperationMode)prop.GetValue(Parameters);
      ComputeShader.SetInt("Operation", (int)value);
    }
  }
}
}