using UnityEngine;
using VoroSystem.Voro.World.ChunkStructure;

namespace VoroSystem.Voro.Compute.EffectSystem.Core {
public abstract class BaseShader {
  public ComputeShader ComputeShader;
  public abstract void SetParameter<T>(string name);

  /// <summary> dispatches the shader </summary>
  public abstract void Compute(Chunk chunk);
}
}