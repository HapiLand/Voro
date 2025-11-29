using UnityEngine;
using VoroSystem.Voro.World.ChunkStructure;

namespace VoroSystem.Voro.Compute.Effects.Core {
public interface IEffect {
  void Compute(Chunk instance);
  Texture2D ReadResult();
  void ConfigureShader();
}
}