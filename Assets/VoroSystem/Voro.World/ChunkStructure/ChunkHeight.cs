using System;
using System.Runtime.InteropServices;
using UnityEngine;
using VoroSystem.Voro.World.ChunkStructure.Interfaces;

namespace VoroSystem.Voro.World.ChunkStructure {
[Serializable]
public class ChunkHeight : IChunkHeight {
  MeshVertex[] _initialVertices;

  public ChunkHeight(ChunkEntity entity) {
    _initialVertices = entity.ChunkMesh.Vertices;
    HeightValues = new float[_initialVertices.Length];
    VertexValues = new Vector3[_initialVertices.Length];

    TryCreateBuffer();
    SetBufferData(_initialVertices);
  }

  #region IChunkHeight Members

  public float[] HeightValues { get; }
  public Vector3[] VertexValues { get; }
  public ComputeBuffer Buffer { get; private set; }

  /// <summary>
  /// find height values
  /// </summary>
  public void ReadBuffer() {
    if (IsReleased || Buffer == null) {
      throw new InvalidOperationException("Attempted to read from a released buffer");
    }

    // get the vertex values
    var positionData = new Vector3[Buffer.count];
    Buffer.GetData(positionData);

    for (var i = 0; i < positionData.Length; i++) {
      // get height
      HeightValues[i] = positionData[i].y;
    }
  }

  public void ReleaseBuffer() {
    if (IsReleased || Buffer == null) {
      return;
    }

    Buffer.Release();
    Buffer = null;
    IsReleased = true;
  }

  public bool IsReleased { get; private set; }

  public void TryCreateBuffer() {
    if (Buffer != null && !IsReleased) {
      return;
    }

    var stride = Marshal.SizeOf(typeof(Vector3));
    Buffer = new ComputeBuffer(_initialVertices.Length, stride);
    IsReleased = false;
    SetBufferData(_initialVertices);
  }

  #endregion

  public void CreateBuffer() { }

  /// <summary>
  /// write every vertex position to the buffer
  /// </summary>
  /// <param name="vertices"> </param>
  void SetBufferData(MeshVertex[] vertices) {
    var array = new Vector3[vertices.Length];
    for (var i = 0; i < array.Length; i++) {
      array[i] = vertices[i].position;
    }

    Buffer.SetData(array);
  }
}
}