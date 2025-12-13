using VoroSystem.Voro.World.ChunkStructure;

namespace VoroSystem.Voro.Compute.EffectSystem.Core {
public abstract class BaseEffect {
  public EffectName Name;
  public abstract void RunEffect(Chunk chunk);
  public abstract void Init();
}
}