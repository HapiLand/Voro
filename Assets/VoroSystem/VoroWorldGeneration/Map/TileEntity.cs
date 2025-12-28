using System;
using UnityEngine;
using Voro.Internal.Terrain.Algorithms;
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
    AlgorithmDispatcher.DispatchOnTile(this);
  }

  public void SetTile(Tile tile) {
    this.tile = tile;
    transform.position = tile.WorldOriginPosition;
    gameObject.name = $"Tile_{tile.WorldOriginPosition.x:F0}_{tile.WorldOriginPosition.z:F0}";

    // Initial material setup
    var mat = new Material(Resources.Load<Material>("TileMat"));
    meshRenderer.sharedMaterial = mat;
    meshRenderer.sharedMaterial.mainTexture = Texture2D.whiteTexture;
    meshFilter.sharedMesh = new MeshBuilder()
      .SetSize(WorldGenTileSettings.TileSize)
      .SetResolution(WorldGenTileSettings.MeshResolution)
      .Build();
  }
}
}