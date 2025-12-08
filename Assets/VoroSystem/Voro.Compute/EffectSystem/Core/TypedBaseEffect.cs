using UnityEngine;
using VoroSystem.Voro.World.ChunkStructure;

namespace VoroSystem.Voro.Compute.EffectSystem.Core {
public abstract class TypedBaseEffect<TParam> : BaseEffect where TParam : class, new()  {
  protected TypedBaseEffect(EffectName name) {
    Name = name;
    Parameters = new TParam();
  }
  public BaseShader Shader { get; protected set; }
  public TParam Parameters { get; }
  
  public override Texture2D RunEffect(Chunk chunk) {
    // Shader.ConfigureShader();
    Shader.Compute(chunk);
    return Shader.ReadResult();
  }

  #region Serialized Fields

  public EffectName Name;
  public OperationMode Mode;

  #endregion
}
}