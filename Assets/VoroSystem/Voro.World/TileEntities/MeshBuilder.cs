using System;
using System.Collections.Generic;
using UnityEngine;
using VoroSystem.Util;
using VoroSystem.Voro.World.Map;

namespace VoroSystem.Voro.World.TileEntities {
[Serializable]
public class MeshBuilder {
  // todo make static
  MeshComponent _meshComponent;
  Vertex[] _vertices;

  public MeshBuilder(MeshComponent meshComponent) {
    _meshComponent = meshComponent;
  }

  public Mesh Mesh { get; private set; }

  public Vertex[] Vertices => _vertices;

  public Mesh BuildMesh(Tile tile) {
    Mesh = new Mesh();
    var size = tile.Size;
    var pos = tile.Position;

    CreateVertices(size, out _vertices);
    CreateTriangles(out var tris);
    CreateUVs(_vertices, out var uvs, pos, size);

    // Assign
    Mesh.vertices = _vertices.ToVector3Array();
    Mesh.triangles = tris.ToArray();
    Mesh.uv = uvs;
    Mesh.RecalculateNormals();
    return Mesh;
  }

  static void CreateVertices(float size, out Vertex[] v) {
    var list = new List<Vertex>();
    var step = size / MeshBase.Subdivision;
    for (var z = 0; z <= MeshBase.Subdivision; z++) {
      for (var x = 0; x <= MeshBase.Subdivision; x++) {
        var px = x * step;
        var pz = z * step;
        list.Add(new Vertex(new Vector3(px, 0f, pz)));
      }
    }

    v = list.ToArray();
  }

  static void CreateTriangles(out List<int> tris) {
    tris = new List<int>();
    var s = MeshBase.Subdivision;

    for (var z = 0; z < s; z++) {
      for (var x = 0; x < s; x++) {
        var i0 = z * (s + 1) + x;
        var i1 = i0 + 1;
        var i2 = i0 + s + 1;
        var i3 = i2 + 1;

        tris.Add(i0);
        tris.Add(i2);
        tris.Add(i1);

        tris.Add(i1);
        tris.Add(i2);
        tris.Add(i3);
      }
    }
  }

  /// <summary>
  /// makes vertices local to (0,0)
  /// </summary>
  static void CreateUVs(Vertex[] v, out Vector2[] uvs, Vector2 quadPos, float quadSize) {
    uvs = new Vector2[v.Length];
    var s = MeshBase.Subdivision;
    var step = quadSize / s;

    for (var z = 0; z <= s; z++) {
      for (var x = 0; x <= s; x++) {
        var u = quadPos.x + x * step;
        var vv = quadPos.y + z * step;
        uvs[z * (s + 1) + x] = new Vector2(u, vv);
      }
    }
  }
}
}