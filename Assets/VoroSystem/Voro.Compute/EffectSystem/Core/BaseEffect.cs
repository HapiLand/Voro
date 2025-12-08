using Mono.Cecil.Cil;
using UnityEngine;
using VoroSystem.Voro.World.ChunkStructure;

namespace VoroSystem.Voro.Compute.EffectSystem.Core {
public abstract class BaseEffect {
  public OperationMode Mode;
  public EffectName Name;
  public abstract Texture2D RunEffect(Chunk chunk);
  public abstract void Init();

  /// <summary>
  /// creates a point buffer using the chunks mesh vertices
  /// only creates the buffer if one does not already exist
  /// </summary>
  /// <param name="chunk"></param>
  public void TryCreateBuffer(ref Chunk chunk) {
    if (!chunk.HasPointBuffer()) {
      // Debug.Log($"Creating new PointBuffer for {chunk.Index}");
      chunk.CreatePointBuffer();
    }
  }
}
}