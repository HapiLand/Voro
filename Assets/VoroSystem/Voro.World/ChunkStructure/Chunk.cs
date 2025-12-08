using System;
using UnityEngine;
using VoroSystem.Voro.World.Components;

namespace VoroSystem.Voro.World.ChunkStructure {
/// <summary>
/// represents an object that exists within a map
/// represents an object that exists within a scene
/// </summary>
[Serializable]
public class Chunk {
  #region Serialized Fields

  [SerializeField] MapTile mapTile;
  [SerializeField] TileEntity tileEntity;
  [SerializeField] VoroMap voroMap;
  [SerializeField] TileState tileState;

  #endregion

  public Chunk(int index, Vector2 position, float size, VoroMap voroMap) {
    mapTile = new MapTile(index, size);
    tileEntity = new TileEntity(position);
    this.voroMap = voroMap;
    tileState = new TileState();
  }

  public Vector2 Position => tileEntity.Position;
  public float Size => mapTile.Size;
  public bool Visible => tileState.Visible;
  public int Index => mapTile.Index;
  public GameObject Entity => tileEntity.Instance;

  public int VertexPerAxis {
  get{
    if (tileEntity?.TileMesh == null) {
      return 11;
    }
    return tileEntity.TileMesh.subdivision + 1;
  }
  }

  public void SetTexture(Texture2D tex) {
    tileEntity.SetTexture(tex);
  }
  public Texture2D GetTexture() {
    return tileEntity.GetTexture();
  }
  public void Init(Transform parent) {
    if (tileState.Initialised) {
      Debug.LogWarning($"Chunk {mapTile.Index} already initialised");
      return;
    }

    tileEntity.CreateInstance(parent, mapTile.Size, voroMap);
    tileState.Initialised = true;
  }

  public void Update() {
    if (!tileState.Initialised) {
      Debug.LogWarning($"Chunk [{mapTile.Index}] not initialised");
      return;
    }

    tileState.UpdateVisibility(tileEntity.Position);
    tileEntity.UpdateHeight();
  }

  public bool HasPointBuffer() {
    return tileEntity.HasPointBuffer();
  }

  public void CreatePointBuffer() {
    tileEntity.CreatePointBuffer();
  }

  public void ReleasePointBuffer() {
    tileEntity.ReleasePointBuffer();
  }

  public ComputeBuffer GetPointBuffer() {
    return tileEntity.GetPointBuffer();
  }

  public void ReadBuffer() {
    tileEntity.ReadHeightFromPointBuffer();
  }
}
}