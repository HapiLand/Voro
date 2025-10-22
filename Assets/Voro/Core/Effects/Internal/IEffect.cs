using UnityEngine;
using Voro.Core.World;

namespace Voro.Core.Effects.Internal {
interface IEffect {
    ComputeShader Shader { get; set; }
    ComputeBuffer Buffer { get; set; }
    void Initialize(ConfigFX config);
    void ConfigureShader();
    void Compute(BaseResult baseResult);
    void ReadResult(BaseResult baseResult);
}
}