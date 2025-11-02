using UnityEngine;
using VoroSystem.World.FX.Configuration;
using VoroSystem.World.Generate;

namespace VoroSystem.World.FX.Base {
public interface IEffect {
    ComputeShader Shader { get; set; }
    ComputeBuffer Buffer { get; set; }
    void Initialize(JConfigFX? config);
    void ConfigureShader();
    void Compute(BaseResult baseResult);
    void ReadResult(BaseResult baseResult);
}
}