using UnityEngine;
using VoroSystem.Voro.World.ChunkStructure;

namespace VoroSystem.Voro.Compute.EffectSystem.Core {
public abstract class BaseShader {
  public ComputeShader ComputeShader;
  public int TextureSize;
  public RenderTexture Texture;
  public abstract void SetParameter<T>(string name);
  
  /// <summary> converts render texture </summary>
  public abstract Texture2D ReadResult();

  /// <summary> dispatches the shader </summary>
  public abstract void Compute(Chunk chunk);


}
}