using UnityEngine;
using VoroSystem.Voro.World.ChunkStructure;

namespace VoroSystem.Voro.Compute.EffectSystem.Core {
public interface IEffect {
  void Compute(Chunk instance);
  Texture2D ReadResult();
  void ConfigureShader();
}
}