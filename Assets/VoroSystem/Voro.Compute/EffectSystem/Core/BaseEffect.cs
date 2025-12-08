using Mono.Cecil.Cil;
using UnityEngine;
using VoroSystem.Voro.World.ChunkStructure;

namespace VoroSystem.Voro.Compute.EffectSystem.Core {
public abstract class BaseEffect {
  public EffectName Name;
  public abstract Texture2D RunEffect(Chunk chunk);
  public abstract void Init();

}
}