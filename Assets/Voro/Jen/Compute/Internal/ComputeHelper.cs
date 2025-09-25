using System.Runtime.InteropServices;
using UnityEngine;

namespace Voro.Jen.Compute.Internal {
public static class ComputeHelper {
    public static ComputeShader LoadShader(string shaderName) {
        return UnityEngine.Resources.Load<ComputeShader>($"{shaderName}");
    }

    public static int GetStride<T>() {
        return Marshal.SizeOf(typeof(T));
    }

    static Vector3Int GetThreadGroupSizes(ComputeShader shader, int kernelIndex = 0) {
        shader.GetKernelThreadGroupSizes(kernelIndex, out var x, out var y, out var z);
        return new Vector3Int((int)x, (int)y, (int)z);
    }

    public static void Dispatch(
        ComputeShader shader, int kernelIndex,
        int numIterationsX, int numIterationsY = 1, int numIterationsZ = 1) {
        var threadGroupSizes = GetThreadGroupSizes(shader, kernelIndex);
        var numGroupsX = Mathf.CeilToInt(numIterationsX / (float)threadGroupSizes.x);
        var numGroupsY = Mathf.CeilToInt(numIterationsY / (float)threadGroupSizes.y);
        var numGroupsZ = Mathf.CeilToInt(numIterationsZ / (float)threadGroupSizes.z);
        shader.Dispatch(kernelIndex, numGroupsX, numGroupsY, numGroupsZ);
    }
}
}