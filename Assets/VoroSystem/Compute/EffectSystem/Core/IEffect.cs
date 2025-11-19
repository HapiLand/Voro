using UnityEngine;
using VoroSystem.Terrain.Chunks;

namespace VoroSystem.Compute.EffectSystem.Core {
public interface IEffect {
  void Compute(ChunkInstance instance);
  Texture2D ReadResult();
  void ConfigureShader();
}
}