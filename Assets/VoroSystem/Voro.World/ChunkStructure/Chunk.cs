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

  [SerializeField] MapChunk mapChunk;
  [SerializeField] ChunkEntity chunkEntity;
  [SerializeField] ChunkState chunkState;

  #endregion

  public Chunk(int index, Vector2 pos, float size, VoroMap voroMap, Transform parent) {
    mapChunk = new MapChunk(index, size);
    chunkEntity = new ChunkEntity(pos, size, parent, voroMap);
    chunkState = new ChunkState();
  }

  public Vector2 Position => chunkEntity.Position;
  public float Size => mapChunk.ChunkSize;
  public bool Visible => chunkState.Visible;
  public int Index => mapChunk.MapIndex;
  public GameObject Entity => chunkEntity.Entity;
  public int VertexPerAxis => chunkEntity.ChunkMesh.subdivision + 1;
  public ComputeBuffer PointBuffer => chunkEntity.PointBuffer;

  public void SetTexture(Texture2D tex) {
    chunkEntity.SetTexture(tex);
  }

  public Texture2D GetTexture() {
    return chunkEntity.GetTexture();
  }


  public void Update() {
    chunkState.UpdateVisibility(chunkEntity.Position);
    chunkEntity.UpdateHeight();
  }

  public void ReleasePointBuffer() {
    chunkEntity.ReleasePointBuffer();
  }
  public void ReadBuffer() {
    chunkEntity.ReadHeightFromPointBuffer();
  }
}
}