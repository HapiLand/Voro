using System;
using UnityEngine;
using VoroSystem.Landscape.WorldMapSystem;
using VoroSystem.Terrain.Chunks.Geometry;
using VoroSystem.Util.Extensions;

namespace VoroSystem.Terrain.Chunks {
/// <summary>
/// Mesh form of a Tile
/// </summary>
[Serializable]
public class Chunk {
  #region Serialized Fields

  /// <summary>
  /// location of the chunk
  /// </summary>
  public Tile tile;

  /// <summary>
  /// does the instance exist?
  /// </summary>
  public bool initialised;

  /// <summary>
  /// has the Chunk changed?
  /// </summary>
  public bool dirty;

  /// <summary>
  /// store the previous visibility value
  /// </summary>
  public bool lastVisibility;

  public ChunkInstance chunkInstance;

  #endregion

  public Chunk(Tile tile) {
    this.tile = tile;
    dirty = false;
    initialised = false;
    lastVisibility = false;
  }

  /// <summary>
  /// quad mesh
  /// </summary>
  public ChunkQuad ChunkQuad => chunkInstance.chunkQuad;

  public void CreateChunkInstance(int index, Transform parent) {
    var go = new GameObject($"[{index}]");
    chunkInstance = go.AddComponent<ChunkInstance>();

    var mf = go.GetComponent<MeshFilter>();

    chunkInstance.chunkQuad = new ChunkQuad(tile);
    mf.sharedMesh = ChunkQuad.quadMesh;

    go.transform.position = tile.position.ToVector3();
    go.transform.SetParent(parent, true);
  }

  public void DestroyChunkInstance() {
    if (chunkInstance == null) {
      return;
    }

    chunkInstance.Remove();
    chunkInstance = null;
  }
}
}