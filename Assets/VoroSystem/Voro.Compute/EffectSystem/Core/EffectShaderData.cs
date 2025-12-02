using System;
using UnityEngine;

namespace VoroSystem.Voro.Compute.EffectSystem.Core {
/// <summary>
/// data for every effect compute shader
/// </summary>
[Serializable]
public struct EffectShaderData {
    /// <summary>
    /// property ID for the result the shader outputs
    /// </summary>
    public int result;

    /// <summary>
    /// size of the render texture
    /// </summary>
    public int textureSize;

    /// <summary>
    /// Kernel to dispatch
    /// </summary>
    public int kernel;

    /// <summary>
    /// texture format for the render texture
    /// </summary>
    public RenderTextureFormat textureFormat;

    public EffectShaderData(int result, int textureSize, int kernel, RenderTextureFormat textureFormat) {
        this.result = result;
        this.textureSize = textureSize;
        this.kernel = kernel;
        this.textureFormat = textureFormat;
    }
}
}