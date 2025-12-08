using System;
using System.Collections.Generic;
using UnityEngine;
using VoroSystem.Voro.Utilities.Extensions;
using VoroSystem.Voro.World.ChunkStructure.Interfaces;
using VoroSystem.Voro.World.Components;

namespace VoroSystem.Voro.World.ChunkStructure {
[Serializable]
public class TileMesh : ITileMesh {
  #region Serialized Fields

  public int subdivision = 10;
  [SerializeField] MeshFilter filter;
  [SerializeField] Mesh mesh;
  [SerializeField] MeshVertex[] vertices;
  [SerializeField] List<int> triangles;
  [SerializeField] Vector2[] uvs;
  [SerializeField] VoroMap voroMap;
  [SerializeField] float size;
  [SerializeField] Vector2 position;
  public ComputeBuffer PointBuffer;
  #endregion

  public TileMesh(GameObject instance, float size, VoroMap map) {
    this.size = size;
    voroMap = map;
    position = instance.transform.position.ToVector2();

    filter = instance.AddComponent<MeshFilter>();
    BuildMesh();
  }

  #region ITileMesh Members

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

  public void UpdateHeight() {
    var uvs = mesh.uv;

    // todo shader write height value to vertices directly
    for (var i = 0; i < vertices.Length; i++) {
      var height = SampleVertexHeight(i);
      var v = vertices[i];
      v.position = new Vector3(v.position.x, height, v.position.z);
      vertices[i] = v;
    }
    
    /*for (var i = 0; i < vertices.Length; i++) {
      var uv = uvs[i];
      var height = SampleHeightAtCoordinate(uv);
      var v = vertices[i];
      v.position = new Vector3(v.position.x, height, v.position.z);
      vertices[i] = v;
    }*/

    mesh.vertices = vertices.ToVector3Array();
    mesh.RecalculateNormals();
  }

  /// <summary>
  /// reads a height value found in this vertex
  /// </summary>
  float SampleVertexHeight(int index) {
    var vertex = vertices[index];
    return vertex.height;
  }

  #endregion

  void CreateUVs(out Vector2[] uvs) {
    uvs = new Vector2[Vertices.Length];
    var step = size / subdivision;
    for (var z = 0; z <= subdivision; z++) {
      for (var x = 0; x <= subdivision; x++) {
        var u = position.x + x * step;
        var v = position.y + z * step;
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

  float SampleHeightAtCoordinate(Vector2 uv) {
    // find the chunk at this coordinate
    var chunk = voroMap.GetChunkAtPosition(uv);
    if (chunk == null) {
      return 0f;
    }

    // read texture in the tile
    var tex = chunk.GetTexture();
    if (!tex) {
      return 0f;
    }

    // sample the texture to get its height
    var localPos = uv - chunk.Position;
    var u = Mathf.Clamp01(localPos.x / size);
    var v = Mathf.Clamp01(localPos.y / size);
    var sample = tex.GetPixelBilinear(u, v);
    return sample.r;
  }

  public MeshVertex GetVertex(int x, int z) {
    if (x < 0 || x > subdivision || z < 0 || z > subdivision) {
      throw new ArgumentOutOfRangeException($"Invalid vertex coordinates: ({x}, {z})");
    }

    var index = z * (subdivision + 1) + x;
    return vertices[index];
  }

  /// <summary>
  /// update height values using computed data
  /// </summary>
  /// <param name="data"></param>
  /// <exception cref="NotImplementedException"></exception>
  public void Apply(MeshVertex.PointData[] data) {
    for (var i = 0; i < data.Length; i++) {
      Vertices[i].height = data[i].Position.y;
    }
  }
}
}