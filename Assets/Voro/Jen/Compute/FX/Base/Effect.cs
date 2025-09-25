using UnityEngine;
using Voro.Jen.Compute.FX.Internal;
using Voro.Jen.Compute.Internal;

namespace Voro.Jen.Compute.FX.Base {
public abstract class Effect<TEffectData> : EffectBase, IEffect {
    protected readonly ComputeShader CS;
    protected readonly int Kernel;
    protected TEffectData Data;

    protected Effect(EffectName name, TEffectData data, string kernelName = "CSMain") {
        Name = name;
        Data = data;
        CS = ComputeHelper.LoadShader("TileCompute");
        Kernel = CS.FindKernel(kernelName);
    }

    public EffectName Name { get; }

    /// <summary>
    ///     shader code here
    /// </summary>
    public abstract void Dispatch(ComputeBuffer pointBuffer, int bufferSize);
}
}