using UnityEngine;
using VoroSystem.Voro.World.TileEntities;

namespace VoroSystem.Voro.Compute.Effects.EffectSystem.Core {
public interface IEffect {
    void Compute(TileEntity instance);
    Texture2D ReadResult();
    void ConfigureShader();
}
}