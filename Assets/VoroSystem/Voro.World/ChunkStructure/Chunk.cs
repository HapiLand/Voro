using System;
using System.Collections.Generic;
using UnityEngine;
using VoroSystem.Voro.Utilities.Cameras;
using VoroSystem.Voro.Utilities.Extensions;
using VoroSystem.Voro.World.Components;

namespace VoroSystem.Voro.World.ChunkStructure {
/// <summary>
/// represents an object that exists within a map
/// represents an object that exists within a scene
/// </summary>
[Serializable]
public class Chunk : IMapTile, ITileEntity, ITileState, ITileMesh, ITileMaterial {
  public Chunk(int index, Vector2 position, float size, VoroMap voroMap) {
    {
      Index = index;
      Size = size;
      Position = position;
    }
    VoroMap = voroMap;
    {
      Visible = false;
      Initialised = false;
    }
  }

  VoroMap VoroMap { get; }

  #region IMapTile Members

  public int Index { get; }
  public float Size { get; }

  #endregion

  #region ITileEntity Members

  public Vector2 Position { get; }
  public GameObject Instance { get; set; }

  #endregion

  #region ITileMaterial Members

  public MeshRenderer Renderer { get; set; }

  public Material GetMaterial() {
    return Renderer.sharedMaterial;
  }

  public void SetMaterial(Material mat) {
    Renderer.sharedMaterial = new Material(mat);
  }

  public Texture2D GetTexture() {
    return GetMaterial().mainTexture as Texture2D;
  }

  public void SetTexture(Texture2D tex) {
    Renderer.sharedMaterial.mainTexture = tex;
  }

  #endregion

  #region ITileMesh Members

  public MeshFilter Filter { get; set; }
  public Mesh Mesh { get; set; }
  public CVertex[] Vertices { get; set; }
  public List<int> Triangles { get; set; }
  public Vector2[] UVs { get; set; }

  #endregion

  #region ITileState Members

  public bool Initialised { get; set; }
  public bool Visible { get; set; }

  public void Update() {
    if (!Initialised) {
      Debug.LogWarning($"Chunk [{Index}] not initialised");
      return;
    }

    UpdateVisibility();
    UpdateHeight();
  }

  public void Init(Transform parent) {
    if (Initialised) {
      Debug.LogWarning($"Chunk {Index} already initialised");
      return;
    }

    {
      Instance = new GameObject($"[{Index}] ({Position.x:F0},{Position.y:F0})");
      Instance.transform.SetParent(parent);
      Instance.transform.position = Position.ToVector3();
    }

    {
      Renderer = Instance.AddComponent<MeshRenderer>();
      SetMaterial(Resources.Load<Material>("ChunkMaterial"));
      SetTexture(Texture2D.redTexture);
    }

    {
      Filter = Instance.AddComponent<MeshFilter>();
      BuildMesh();
    }
    Initialised = true;
  }

  #endregion

  void BuildMesh() {
    {
      Mesh = new Mesh();

      CreateVertices(out var vtx);
      Vertices = vtx;

      void CreateVertices(out CVertex[] v) {
        var list = new List<CVertex>();
        var step = Size / MeshBase.Subdivision;
        for (var z = 0; z <= MeshBase.Subdivision; z++) {
          for (var x = 0; x <= MeshBase.Subdivision; x++) {
            var px = x * step;
            var pz = z * step;
            list.Add(new CVertex(new Vector3(px, 0f, pz)));
          }
        }

        v = list.ToArray();
      }

      CreateTriangles(out var tri);
      Triangles = tri;

      void CreateTriangles(out List<int> t) {
        t = new List<int>();
        for (var z = 0; z < MeshBase.Subdivision; z++) {
          for (var x = 0; x < MeshBase.Subdivision; x++) {
            var i0 = z * (MeshBase.Subdivision + 1) + x;
            var i1 = i0 + 1;
            var i2 = i0 + MeshBase.Subdivision + 1;
            var i3 = i2 + 1;

            t.Add(i0);
            t.Add(i2);
            t.Add(i1);

            t.Add(i1);
            t.Add(i2);
            t.Add(i3);
          }
        }
      }

      CreateUVs(out var uv);
      UVs = uv;

      void CreateUVs(out Vector2[] uvs) {
        uvs = new Vector2[Vertices.Length];
        var step = Size / MeshBase.Subdivision;
        for (var z = 0; z <= MeshBase.Subdivision; z++) {
          for (var x = 0; x <= MeshBase.Subdivision; x++) {
            var u = Position.x + x * step;
            var v = Position.y + z * step;
            uvs[z * (MeshBase.Subdivision + 1) + x] = new Vector2(u, v);
          }
        }
      }
    }

    {
      Mesh.vertices = Vertices.ToVector3Array();
      Mesh.triangles = Triangles.ToArray();
      Mesh.uv = UVs;
      Mesh.RecalculateNormals();
      Filter.sharedMesh = Mesh;
    }
  }

  void UpdateHeight() {
    var uvs = Mesh.uv;

    for (var i = 0; i < Vertices.Length; i++) {
      var uv = uvs[i];
      var height = SampleHeightAtCoordinate(uv);
      var v = Vertices[i];
      v.position = new Vector3(v.position.x, height, v.position.z);
      Vertices[i] = v;
    }

    Mesh.vertices = Vertices.ToVector3Array();
    Mesh.RecalculateNormals();
  }

  float SampleHeightAtCoordinate(Vector2 uv) {
    // find the tile at this coordinate
    var tile = VoroMap.GetChunkAtPosition(uv);
    if (tile == null) {
      return 0f;
    }

    // read texture in the tile
    var tex = tile.GetTexture();
    if (!tex) {
      return 0f;
    }

    // sample the texture to get its height
    var localPos = uv - tile.Position;
    var u = Mathf.Clamp01(localPos.x / Size);
    var v = Mathf.Clamp01(localPos.y / Size);
    var sample = tex.GetPixelBilinear(u, v);
    return sample.r;
  }

  void UpdateVisibility() {
    var cam = CameraManager.Camera;
    var tileWorldPos = Position.ToVector3();
    var viewportPos = cam.WorldToViewportPoint(tileWorldPos);
    var isVisible = viewportPos is { z: > 0, x: >= 0 and <= 1, y: >= 0 and <= 1 };
    Visible = isVisible;
  }
}
}