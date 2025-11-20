using System;
using UnityEngine;

namespace VoroSystem.Voro.World.Voro.Terrain.Ground.Chunks.Geometry {
[Serializable]
public class QuadDisplacement {
  ChunkQuad _chunkQuad;

  public QuadDisplacement(ChunkQuad chunkQuad) {
    _chunkQuad = chunkQuad;
  }

  public void DisplaceVertices(Func<QuadVertex, float> heightFunc) {
    for (var i = 0; i < _chunkQuad.vertices.Length; i++) {
      var v = _chunkQuad.vertices[i];
      var height = heightFunc(v);
      v.position = new Vector3(v.position.x, height, v.position.z);
      _chunkQuad.vertices[i] = v;
    }

    UpdateVertices();
  }

  void UpdateVertices() {
    _chunkQuad.quadMesh.vertices = _chunkQuad.vertices.ToVector3Array();
    _chunkQuad.quadMesh.RecalculateNormals();
  }
}
}