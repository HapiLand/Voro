using UnityEngine;
using VoroSystem.Generation.GraphSystem.Graph;
using VoroSystem.Generation.MesherSystem;

namespace VoroSystem.Generation.DiagramSystem.Effects {
public interface IEffect {
    ComputeShader Shader { get; set; }
    ComputeBuffer Buffer { get; set; }
    void Initialize(LayerEffect effect);
    void ConfigureShader();
    void Compute(BaseResult baseResult);
    void ReadResult(BaseResult baseResult);
}
}