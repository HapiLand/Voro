using System;
using UnityEngine;
using VoroSystem.VoroWorldGeneration.HeightSystem;

namespace VoroSystem.VoroWorldGeneration.Map {
[Serializable]
[ExecuteAlways]
[RequireComponent(typeof(MeshRenderer), typeof(MeshFilter))]
public class TileEntity : MonoBehaviour {
  #region Serialized Fields
  [SerializeField] MeshFilter meshFilter;
  [SerializeField] MeshRenderer meshRenderer;
  [SerializeField] Tile tile;
  [field: SerializeField] public bool IsDirty { get; private set; }
  #endregion

  #region Event Functions
  void Awake() {
    meshRenderer = GetComponent<MeshRenderer>();
    meshFilter = GetComponent<MeshFilter>();
  }
  #endregion

  public void UpdateTileEntity() {
    if (tile == null) {
      return;
    }

    UpdateHeightSystem();
    ClearDirty();
  }

  public void MarkDirty() {
    IsDirty = true;
  }

  void ClearDirty() {
    IsDirty = false;
  }

  void UpdateHeightSystem() {
    var samplerFunc = TerrainHeightSystem.SampleHeight(this);
    samplerFunc((position, height) => { });
  }

  public void SetTile(Tile tile) {
    this.tile = tile;
    transform.position = new Vector3(tile.Position.x, 0f, tile.Position.y);
    gameObject.name = $"Tile_{tile.Position.x}_{tile.Position.y}";

    // Initial material setup
    var mat = new Material(Resources.Load<Material>("ChunkMaterial"));
    meshRenderer.sharedMaterial = mat;
    meshRenderer.sharedMaterial.mainTexture = Texture2D.whiteTexture;
    meshFilter.sharedMesh = new MeshBuilder()
      .SetSize(WorldGenTileSettings.TileSize)
      .SetResolution(WorldGenTileSettings.MeshResolution)
      .Build();
  }
  /*void Start() {
    if (_tile == null) {
      Debug.LogError("TileEntity: _tile is null");
      return;
    }
    SetMaterial(new Material(Resources.Load<Material>("ChunkMaterial")));
    SetTexture(Texture2D.whiteTexture);
    SetMesh(new MeshBuilder()
      .SetSize(WorldGenTileSettings.TileSize)
      .SetResolution(WorldGenTileSettings.MeshResolution)
      .Build());
    MarkDirty();
  }*/

  /*void Update() {
    if (_tile == null) {
      return;
    }

    UpdateVisibility();
    if (!_dirty) {
      return;
    }

    UpdateHeightSystem();
    ClearDirty();
  }*/

  /*void UpdateVisibility() {
    var viewportPos = Camera.main.WorldToViewportPoint(gameObject.transform.position);
    _visible = viewportPos is { z: > 0, x: >= 0 and <= 1, y: >= 0 and <= 1 };
    SetTexture(_visible ? Texture2D.whiteTexture : Texture2D.redTexture);
  }

  public void UpdateHeightSystem() {
    var samplerFunc = TerrainHeightSystem.SampleHeight(this);
    var displaced = samplerFunc((position, height) => { });
  }

  void MarkDirty() {
    _dirty = true;
  }

  void ClearDirty() {
    _dirty = false;
  }

  void SetMaterial(Material mat) {
    _mr.sharedMaterial = mat;
  }

  void SetTexture(Texture2D tex) {
    _mr.sharedMaterial.mainTexture = tex;
  }

  void SetMesh(Mesh mesh) {
    _mf.sharedMesh = mesh;
  }

  public void SetTile(Tile tile) {
    Debug.Log("TileEntity Set Tile");
    _tile = tile;
    transform.position = new Vector3(tile.Position.x, 0f, tile.Position.y);
    gameObject.name = $"Tile_{tile.Position.x}_{tile.Position.y}";
  }*/
}
}