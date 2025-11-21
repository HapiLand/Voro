using System;
using UnityEngine;
using VoroSystem.Util.Extensions;
using VoroSystem.Voro.World.Map;
using VoroSystem.Voro.World.TerrainOLD.Ground.Chunks.Geometry;

namespace VoroSystem.Voro.World.TerrainOLD.Ground.Chunks {
/// <summary>
/// Mesh form of a Tile
/// </summary>
[Serializable]
public class Chunk : IChunk {
  #region Serialized Fields

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

  #endregion

  public Chunk(Tile tile) {
    Tile = tile;
    dirty = false;
    initialised = false;
    lastVisibility = false;
  }

  /// <summary>
  /// quad mesh
  /// </summary>
  public ChunkQuad ChunkQuad => Instance.chunkQuad;

  #region IChunk Members

  /// <summary>
  /// location of the chunk
  /// </summary>
  // public Tile Tile;
  public ITile Tile { get; }

  // public ChunkInstance chunkInstance;
  public ChunkInstance Instance { get; set; }

  #endregion

  public void CreateChunkInstance(int index, Transform parent) {
    var go = new GameObject($"[{index}]");
    Instance = go.AddComponent<ChunkInstance>();

    var mf = go.GetComponent<MeshFilter>();

    Instance.chunkQuad = new ChunkQuad(Tile);
    mf.sharedMesh = ChunkQuad.quadMesh;

    go.transform.position = Tile.Position.ToVector3();
    go.transform.SetParent(parent, true);
  }

  public void DestroyChunkInstance() {
    if (Instance == null) {
      return;
    }

    Instance.Remove();
    Instance = null;
  }
}
}