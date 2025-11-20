using UnityEngine;
using VoroSystem.Voro.World.Voro.Terrain.Ground.Chunks;

namespace VoroSystem.Voro.Compute.Effects.EffectSystem.Core {
public interface IEffect {
  void Compute(ChunkInstance instance);
  Texture2D ReadResult();
  void ConfigureShader();
}
}