using UnityEngine;

namespace VoroSystem.Terrain {
/// <summary>
///     base class for an effect, which is a terrain generation function
/// </summary>
public abstract class EffectBase {
    public abstract EffectName Name { get; }
    public abstract void Dispatch(ComputeBuffer buffer, int bufferSize);
}
}