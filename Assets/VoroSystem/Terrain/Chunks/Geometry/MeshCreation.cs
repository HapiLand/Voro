using System;
using System.Collections.Generic;
using UnityEngine;

namespace VoroSystem.Terrain.Chunks.Geometry {
[Serializable]
public class MeshCreation {
  #region Serialized Fields

  [SerializeField] ChunkQuad chunkQuad;

  #endregion

  public MeshCreation(ChunkQuad chunkQuad) {
    this.chunkQuad = chunkQuad;
  }

  public Mesh CreateMesh(float quadSize, Vector2 quadPosition) {
    var mesh = new Mesh();

    CreateVertices(quadSize, out chunkQuad.vertices);
    CreateTriangles(out var triangles);
    CreateUVs(chunkQuad.vertices, out var uvs, quadPosition, quadSize);

    // Assign
    mesh.vertices = chunkQuad.vertices.ToVector3Array();
    mesh.triangles = triangles.ToArray();
    mesh.uv = uvs;
    mesh.RecalculateNormals();
    return mesh;
  }

  static void CreateUVs(QuadVertex[] vertices, out Vector2[] uvs, Vector2 quadPosition, float quadSize) {
    uvs = new Vector2[vertices.Length];
    for (var z = 0; z < QuadBase.QuadDensity + 1; z++) {
      for (var x = 0; x < QuadBase.QuadDensity + 1; x++) {
        var u = quadPosition.x + x * (quadSize / QuadBase.QuadDensity);
        var v = quadPosition.y + z * (quadSize / QuadBase.QuadDensity);

        uvs[z * (QuadBase.QuadDensity + 1) + x] = new Vector2(u, v);
      }
    }
  }

  static void CreateTriangles(out List<int> triangles) {
    triangles = new List<int>();
    for (var z = 0; z < QuadBase.QuadDensity; z++) {
      for (var x = 0; x < QuadBase.QuadDensity; x++) {
        var i0 = z * (QuadBase.QuadDensity + 1) + x;
        var i1 = i0 + 1;
        var i2 = i0 + QuadBase.QuadDensity + 1;
        var i3 = i2 + 1;

        AddTriangle(triangles, i0, i2, i1);
        AddTriangle(triangles, i1, i2, i3);
      }
    }

    return;

    void AddTriangle(List<int> triangles, int i0, int i1, int i2) {
      triangles.Add(i0);
      triangles.Add(i1);
      triangles.Add(i2);
    }
  }

  /// <summary>
  /// makes vertices local to (0,0)
  /// </summary>
  /// <param name="size"> size of the quad </param>
  /// <param name="vertices"> the vertex array </param>
  static void CreateVertices(float size, out QuadVertex[] vertices) {
    var list = new List<QuadVertex>();
    var step = size / QuadBase.QuadDensity;
    for (var z = 0; z < QuadBase.QuadDensity + 1; z++) {
      for (var x = 0; x < QuadBase.QuadDensity + 1; x++) {
        AddVertex(x, z);
      }
    }

    vertices = list.ToArray();
    return;

    void AddVertex(int x, int z) {
      var vx = x * step;
      var vz = z * step;
      list.Add(new QuadVertex(new Vector3(vx, 0f, vz)));
    }
  }
}
}