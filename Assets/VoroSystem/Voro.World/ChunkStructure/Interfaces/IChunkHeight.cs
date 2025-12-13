using UnityEngine;

namespace VoroSystem.Voro.World.ChunkStructure.Interfaces {
public interface IChunkHeight {
  /// <summary> values to set position.y from </summary>
  float[] HeightValues { get; }

  /// <summary> positions of the vertices to write to buffer </summary>
  Vector3[] VertexValues { get; }

  ComputeBuffer Buffer { get; }
  bool IsReleased { get; }
  void ReadBuffer();
  void ReleaseBuffer();
  void TryCreateBuffer();
}
}