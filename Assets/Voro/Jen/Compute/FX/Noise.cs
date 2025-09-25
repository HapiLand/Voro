using System;
using UnityEngine;
using Voro.Jen.Compute.FX.Base;
using Voro.Jen.Compute.FX.Internal;
using Voro.Jen.Compute.Internal;

namespace Voro.Jen.Compute.FX {
[Serializable]
public class NoiseData : IEffectData {
    public float Scale;
}

public class Noise : Effect<NoiseData> {
    public Noise(NoiseData data) : base(EffectName.Noise, data) {
        data.Scale = 0f;
    }

    int EffectType => 1;

    public override void Dispatch(ComputeBuffer buffer, int bufferSize) {
        CS.SetBuffer(Kernel, Shader.PropertyToID("_Points"), buffer);
        CS.SetInt(Shader.PropertyToID("_PointCount"), bufferSize);

        CS.SetInt(Shader.PropertyToID("_EffectType"), EffectType);
        CS.SetFloat(Shader.PropertyToID("_EffectData"), Data.Scale);

        Debug.Log($"Dispatching Effect {Name.ToString()}");
        ComputeHelper.Dispatch(CS, Kernel, bufferSize);
    }
}
}