using System;
using System.Collections.Generic;
using UnityEngine;
using VoroSystem.Voro.Utilities.Extensions;
using VoroSystem.Voro.World.ChunkStructure.Interfaces;
using VoroSystem.Voro.World.Components;

namespace VoroSystem.Voro.World.ChunkStructure {
[Serializable]
public class ChunkMesh : IChunkMesh {
  #region Serialized Fields
  public int subdivision = 11;
  [SerializeField] MeshFilter filter;
  [SerializeField] Mesh mesh;
  [SerializeField] MeshVertex[] vertices;
  [SerializeField] List<int> triangles;
  [SerializeField] Vector2[] uvs;
  [SerializeField] VoroMap voroMap;
  [SerializeField] float size;
  [SerializeField] Vector3 position;
  #endregion

  public ChunkMesh(GameObject instance, float size, VoroMap map) {
    this.size = size;
    voroMap = map;
    position = instance.transform.position;

    filter = instance.AddComponent<MeshFilter>();
    BuildMesh();
  }

  #region IChunkMesh Members
  public MeshFilter Filter => filter;
  public Mesh Mesh => mesh;
  public MeshVertex[] Vertices => vertices;
  public List<int> Triangles => triangles;
  public Vector2[] UVs => uvs;

  public void BuildMesh() {
    mesh = new Mesh();

    CreateVertices(out var vtx);
    vertices = vtx;

    CreateTriangles(out var tri);
    triangles = tri;

    CreateUVs(out var uv);
    uvs = uv;

    mesh.vertices = vertices.ToVector3Array();
    mesh.triangles = triangles.ToArray();
    mesh.uv = uvs;
    mesh.RecalculateNormals();
    filter.sharedMesh = mesh;
  }
  #endregion

  void CreateUVs(out Vector2[] uvs) {
    uvs = new Vector2[vertices.Length];
    var step = size / subdivision;
    for (var z = 0; z <= subdivision; z++) {
      for (var x = 0; x <= subdivision; x++) {
        var u = position.x + x * step;
        var v = position.z + z * step;
        uvs[z * (subdivision + 1) + x] = new Vector2(u, v);
      }
    }
  }

  void CreateTriangles(out List<int> t) {
    t = new List<int>();
    for (var z = 0; z < subdivision; z++) {
      for (var x = 0; x < subdivision; x++) {
        var i0 = z * (subdivision + 1) + x;
        var i1 = i0 + 1;
        var i2 = i0 + subdivision + 1;
        var i3 = i2 + 1;

        NewTriangle(ref t, i0, i2, i1);
        NewTriangle(ref t, i1, i2, i3);
      }
    }

    return;

    void NewTriangle(ref List<int> list, int i1, int i2, int i3) {
      list.Add(i1);
      list.Add(i2);
      list.Add(i3);
    }
  }

  void CreateVertices(out MeshVertex[] v) {
    var list = new List<MeshVertex>();
    var step = size / subdivision;
    for (var z = 0; z <= subdivision; z++) {
      for (var x = 0; x <= subdivision; x++) {
        var px = x * step;
        var pz = z * step;
        list.Add(new MeshVertex(new Vector3(px, 0f, pz)));
      }
    }

    v = list.ToArray();
  }

  /// <summary>
  /// update height values using computed data
  /// </summary>
  /// <param name="data"> </param>
  public void Apply(float[] data) {
    for (var i = 0; i < data.Length; i++) {
      vertices[i].height = data[i];
    }

    mesh.vertices = vertices.ToVector3Array();
    mesh.RecalculateNormals();
  }
}
}