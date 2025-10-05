using System.Collections.Generic;
using UnityEngine;

namespace VoroSystem {
/// <summary>
///     handles loading and dispatching for an effects shader
/// </summary>
public class EffectShaderWrapper {
    public EffectShaderWrapper(string shaderName, int kernel = 0) {
        Debug.Log($"new EffectShaderWrapper {shaderName}");
        CS = LoadShader(shaderName);
        Kernel = kernel;
    }

    public ComputeShader CS { get; }
    public int Kernel { get; }

    ComputeShader LoadShader(string shaderName) {
        return Resources.Load<ComputeShader>($"{shaderName}");
    }

    public void Dispatch(Dictionary<string, object> parameters, int threadCount) {
        foreach (var kv in parameters) {
            // write the parameter to the shader so it can be dispatched
            switch (kv.Value) {
            case float f: CS.SetFloat(Shader.PropertyToID(kv.Key), f); break;
            case int i: CS.SetInt(Shader.PropertyToID(kv.Key), i); break;
            case ComputeBuffer buf: CS.SetBuffer(Kernel, Shader.PropertyToID(kv.Key), buf); break;
            }
        }

        var threadGroupSizes = GetThreadGroupSizes(CS);
        var numGroupsX = Mathf.CeilToInt(threadCount / (float)threadGroupSizes.x);
        var numGroupsY = Mathf.CeilToInt(1 / (float)threadGroupSizes.y);
        var numGroupsZ = Mathf.CeilToInt(1 / (float)threadGroupSizes.z);
        CS.Dispatch(0, numGroupsX, numGroupsY, numGroupsZ);
    }

    static Vector3Int GetThreadGroupSizes(ComputeShader shader, int kernelIndex = 0) {
        shader.GetKernelThreadGroupSizes(kernelIndex, out var x, out var y, out var z);
        return new Vector3Int((int)x, (int)y, (int)z);
    }
}
}