using System;
using UnityEngine;
using VoroSystem.Voro.Compute.Graphs;
using VoroSystem.Voro.World.ChunkStructure;

namespace VoroSystem.Voro.Compute.Effects.Core {
[Serializable]
public abstract class EffectManager {
    protected IEffect Effect;
    public abstract string Name { get; }
    protected abstract IEffect MakeEffect(Node node);

    public Texture2D RunEffect(Chunk instance) {
        Effect.ConfigureShader();
        Effect.Compute(instance);
        return Effect.ReadResult();
    }
}
}