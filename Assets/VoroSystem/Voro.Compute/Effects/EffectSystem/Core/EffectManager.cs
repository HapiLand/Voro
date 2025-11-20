using System;
using UnityEngine;
using VoroSystem.Voro.Designer.Graph;
using VoroSystem.Voro.World.Voro.Terrain.Ground.Chunks;

namespace VoroSystem.Voro.Compute.Effects.EffectSystem.Core {
[Serializable]
public abstract class EffectManager {
  protected IEffect Effect;
  public abstract string Name { get; }
  protected abstract IEffect MakeEffect(Node node);

  public Texture2D RunEffect(ChunkInstance instance) {
    Effect.ConfigureShader();
    Effect.Compute(instance);
    return Effect.ReadResult();
  }
}
}