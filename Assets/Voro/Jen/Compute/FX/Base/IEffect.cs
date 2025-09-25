using UnityEngine;
using Voro.Jen.Compute.FX.Internal;

namespace Voro.Jen.Compute.FX.Base {
public interface IEffect {
    EffectName Name { get; }
    void Dispatch(ComputeBuffer pointBuffer, int bufferSize);
}
}