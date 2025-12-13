using UnityEngine;
using VoroSystem.Voro.World.ChunkStructure;

namespace VoroSystem.Voro.Compute.EffectSystem.Core {
public abstract class TypedBaseEffect<TParam> : BaseEffect where TParam : class, new() {
  protected TypedBaseEffect(EffectName name) {
    Name = name;
    Parameters = new TParam();
  }

  public BaseShader Shader { get; protected set; }
  public TParam Parameters { get; }

  public override void RunEffect(Chunk chunk) {
    Debug.Log($"Run Effect: {Name.ToString()}");
    Shader.Compute(chunk);
  }
}
}