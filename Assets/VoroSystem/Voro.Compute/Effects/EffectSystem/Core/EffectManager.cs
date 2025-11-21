using System;
using UnityEngine;
using VoroSystem.Voro.Designer.Canvas;
using VoroSystem.Voro.World.TileEntities;

namespace VoroSystem.Voro.Compute.Effects.EffectSystem.Core {
[Serializable]
public abstract class EffectManager {
    protected IEffect Effect;
    public abstract string Name { get; }
    protected abstract IEffect MakeEffect(Node node);

    public Texture2D RunEffect(TileEntity instance) {
        Effect.ConfigureShader();
        Effect.Compute(instance);
        return Effect.ReadResult();
    }
}
}