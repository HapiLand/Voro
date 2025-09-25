using System;
using UnityEngine;
using Voro.Jen.Compute.FX.Base;
using Voro.Jen.Compute.FX.Internal;
using Voro.Jen.Compute.Internal;

namespace Voro.Jen.Compute.FX {
[Serializable]
public class ConstantHeightData : IEffectData {
    public float Height;
}

public class ConstantHeight : Effect<ConstantHeightData> {
    public ConstantHeight(ConstantHeightData data) : base(EffectName.ConstantHeight, data) {
        data.Height = 0f;
    }

    int EffectType => 0;

    public override void Dispatch(ComputeBuffer buffer, int bufferSize) {
        CS.SetBuffer(Kernel, Shader.PropertyToID("_Points"), buffer);
        CS.SetInt(Shader.PropertyToID("_PointCount"), bufferSize);

        CS.SetInt(Shader.PropertyToID("_EffectType"), EffectType);
        CS.SetFloat(Shader.PropertyToID("_EffectData"), Data.Height);

        Debug.Log($"Dispatching Effect {Name.ToString()}");
        ComputeHelper.Dispatch(CS, Kernel, bufferSize);
    }
}
}