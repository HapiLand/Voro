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
  [SerializeField] ChunkHeight chunkHeightmap;

  #endregion

  public Chunk(int index, Vector3 position, float size, VoroMap voroMap, Transform parent) {
    mapChunk = new MapChunk(index, size);
    chunkEntity = new ChunkEntity(position, size, parent, voroMap);
    chunkState = new ChunkState();
    chunkHeightmap = new ChunkHeight(chunkEntity);
  }

  public Vector3 Position => chunkEntity.Position;
  public float Size => mapChunk.ChunkSize;
  public bool Visible => chunkState.Visible;
  public int Index => mapChunk.MapIndex;
  public GameObject Entity => chunkEntity.Entity;
  public int VertexPerAxis => chunkEntity.ChunkMesh.subdivision + 1;
  public ComputeBuffer PointBuffer => chunkHeightmap.Buffer;
  public Texture2D Texture => chunkEntity.GetTexture();

  public void Update() {
    chunkState.UpdateVisibility(chunkEntity.Position);
  }

  public void TryCreateBuffer() {
    chunkHeightmap.TryCreateBuffer();
  }

  /// <summary>
  /// set position of vertices using buffer
  /// </summary>
  public void ApplyBuffer() {
    // get height values
    chunkHeightmap.ReadBuffer();
    chunkEntity.ChunkMesh.Apply(chunkHeightmap.HeightValues);
    chunkHeightmap.ReleaseBuffer();
  }

  public void SetTexture(Texture2D computeFunc) { }
}
}