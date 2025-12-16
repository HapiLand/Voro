using System.Collections.Generic;
using UnityEngine;

namespace VoroSystem.VoroWorldGeneration.Map {
public class MeshBuilder {
  List<int> _triangles;
  Vector2[] _uvs;
  Vector3[] _vertices;

  Mesh Mesh { get; } = new();
  float Size { get; set; } = 1f;
  int Resolution { get; set; } = 1;

  public MeshBuilder SetSize(float size) {
    Size = size;
    return this;
  }

  public MeshBuilder SetResolution(int resolution) {
    Resolution = Mathf.Max(1, resolution);
    return this;
  }


  public Mesh Build() {
    var vertexCount = (Resolution + 1) * (Resolution + 1);
    _vertices = new Vector3[vertexCount];
    _uvs = new Vector2[vertexCount];
    _triangles = new List<int>(Resolution * Resolution * 6);

    var step = Size / Resolution;

    // Generate vertices and UVs
    for (var z = 0; z <= Resolution; z++) {
      for (var x = 0; x <= Resolution; x++) {
        var i = z * (Resolution + 1) + x;
        _vertices[i] = new Vector3(x * step, 0f, z * step);
        _uvs[i] = new Vector2((float)x / Resolution, (float)z / Resolution); // normalized 0..1
      }
    }

    // Generate triangles
    for (var z = 0; z < Resolution; z++) {
      for (var x = 0; x < Resolution; x++) {
        var i0 = z * (Resolution + 1) + x;
        var i1 = i0 + 1;
        var i2 = i0 + Resolution + 1;
        var i3 = i2 + 1;

        // Triangle 1
        _triangles.Add(i0);
        _triangles.Add(i2);
        _triangles.Add(i1);

        // Triangle 2
        _triangles.Add(i1);
        _triangles.Add(i2);
        _triangles.Add(i3);
      }
    }

    // Apply to mesh
    Mesh.Clear();
    Mesh.vertices = _vertices;
    Mesh.triangles = _triangles.ToArray();
    Mesh.uv = _uvs;
    Mesh.RecalculateNormals();
    Mesh.RecalculateBounds();
    return Mesh;
  }
}
}