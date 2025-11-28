using UnityEngine;
using VoroSystem.Util;
using VoroSystem.Util.Extensions;
using VoroSystem.Voro.World.Map;

namespace VoroSystem.Voro.World.TileEntities {
[ExecuteAlways]
public class MeshComponent : MonoBehaviour {
  TileEntity _entity;
  MaterialComponent _materialComponent;
  MeshBuilder _meshBuilder;
  MeshFilter _meshFilter;
  MeshRenderer _meshRenderer;
  VoroWorld _world;
  VoroMap _map;
  
  #region Event Functions

  void Awake() {
    _meshBuilder = new MeshBuilder(this);
    _meshFilter = GetComponent<MeshFilter>();
    _meshRenderer = GetComponent<MeshRenderer>();
    _materialComponent = GetComponent<MaterialComponent>();
  }

  #endregion

  public void Initialize(TileEntity entity, VoroWorld world, VoroMap map) {
    _entity = entity;
    _materialComponent.Initialize();
    _meshFilter.sharedMesh = _meshBuilder.BuildMesh(entity.Tile);
    _map = map;
    _world = world;
  }

  public void UpdateHeight() {
    var tex = GetTileTexture();
    if (!tex) {
      return;
    }
    if (!_map) {
      return;
    }

    var mesh = _meshBuilder.Mesh;
    var vertices = _meshBuilder.Vertices;
    var uvs = mesh.uv;

    for (var i = 0; i < vertices.Length; i++) {
      var uv = uvs[i];
      var height = SampleHeightAtCoordinate(tex, uv);
      var v = vertices[i];
      v.position = new Vector3(v.position.x, height, v.position.z);
      vertices[i] = v;
    }

    mesh.vertices = vertices.ToVector3Array();
    mesh.RecalculateNormals();
   
  }

  Texture2D GetTileTexture() {
    var material = _meshRenderer.sharedMaterial;
    var tex = material.mainTexture as Texture2D;
    return tex;
  }

  static float SampleHeightAtCoordinate(Texture2D tex, Vector2 uv) {
    var sample = tex.GetPixelBilinear(uv.x, uv.y);
    var h = sample.r;
    return h;
  }
}
}