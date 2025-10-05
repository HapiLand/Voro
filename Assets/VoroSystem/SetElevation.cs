using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using Debug = UnityEngine.Debug;

namespace VoroSystem {
/// <summary>
///     sets height to a constant value, a flat plane
/// </summary>
public class SetElevation : EffectBase {
    readonly float Amount;
    readonly EffectShaderWrapper ShaderWrapper;

    public SetElevation(Control[] data) {
        Amount = data[0].Value; // set the data value for the effect
        ShaderWrapper = new EffectShaderWrapper("SetElevation"); // gets the compute shader for the effect
        Debug.Log("Created new SetElevation Effect");
    }

    public override EffectName Name => EffectName.SetElevation;

    public override void Dispatch(ComputeBuffer buffer, int bufferSize) {
        Debug.Log("Setting parameters in SetElevation compute");
        // create the parameters that drive the compute shader
        var parameters = new Dictionary<string, object>
        {
            { "points", buffer }, // compute buffer stores points from the diagram map
            { "point_count", bufferSize }, // number of points in the map
            { "amount", Amount } // the parameter for the function to do its thing todo as cbuffer
        };

        Debug.Log("Dispatching SetElevation");
        var sw = new Stopwatch();
        sw.Start();
        ShaderWrapper.Dispatch(parameters, bufferSize);
        sw.Stop();
        Debug.Log($"took {sw.ElapsedMilliseconds}ms to dispatch");
    }
}
}