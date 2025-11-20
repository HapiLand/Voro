using System.Collections.Generic;
using UnityEngine;
using VoroSystem.Util;
using VoroSystem.Voro.World.Landscape;
using VoroSystem.Voro.World.Voro.Terrain.Ground.Chunks;

namespace VoroSystem.Voro.World.Voro.Terrain {
/// <summary>
/// Generates the 3D form of the landscape
/// </summary>
public class ChunkTerrain : MonoBehaviour {
  #region Serialized Fields

  [SerializeField] VoroLandscape landscape;
  [SerializeField] public Chunk[] chunkMap;

  [SerializeField] public int sizeX;
  [SerializeField] public int sizeZ;

  #endregion

  #region Event Functions

  void Update() {
    Generate();
  }

  void OnEnable() {
    if (chunkMap == null) {
      return;
    }

    // destroy all instances in order to regenerate
    for (var i = chunkMap.Length - 1; i >= 0; i--) {
      var c = chunkMap[i];
      c.DestroyChunkInstance();
      c.lastVisibility = false;
    }

    // fully regenerate the existing terrain
    InitTerrain();
  }

  #endregion

  public void Initialize(VoroLandscape landscape) {
    this.landscape = landscape;
    sizeX = this.landscape.MapXSize;
    sizeZ = this.landscape.MapZSize;
    InitTerrain();
  }

  /// <summary>
  /// Creates the initial arrays
  /// </summary>
  void InitTerrain() {
    var count = sizeX * sizeZ;
    chunkMap = new Chunk[count];

    for (var z = 0; z < sizeZ; z++) {
      for (var x = 0; x < sizeX; x++) {
        CreateChunk(x, z);
      }
    }
  }

  /// <summary>
  /// Makes a new uninitialised Chunk
  /// </summary>
  void CreateChunk(int x, int z) {
    var index = HelperUtility.GetIndex(x, z, sizeX);
    var tile = landscape.GetTile(index);
    chunkMap[index] = new Chunk(tile);
  }

  /// <summary>
  /// Instances the meshes for every chunk
  /// </summary>
  void Generate() {
    foreach (var info in EnumerateChunks()) {
      if (!info.isInitialised) {
        // No instance exists yet
        var exists = CreateInstance((info.index, info.chunk));

        if (!exists) {
          // this chunk was not instanced
          continue;
        }
      }

      var currentVisibility = info.chunk.tile.Visible;
      var lastVisibility = info.chunk.lastVisibility;

      if (currentVisibility != lastVisibility) {
        // Visibility has changed in the Chunk, set it as Dirty
        info.chunk.dirty = true;
      }

      if (info.isDirty) {
        // Chunk needs to be updated
        UpdateInstance(info.chunk);
      }

      // Store latest value for the next frame
      info.chunk.lastVisibility = currentVisibility;
    }
  }

  static void UpdateInstance(Chunk chunk) {
    var currentVisibility = chunk.tile.Visible;

    if (!currentVisibility) {
      // remove the instance that is not visible
      chunk.DestroyChunkInstance();
      // reset the initialised value
      chunk.initialised = false;
    }

    // clean chunk as it has been updated
    chunk.dirty = false;
  }

  IEnumerable<(int index, Chunk chunk, bool isVisible, bool isDirty, bool isInitialised)> EnumerateChunks() {
    for (var i = chunkMap.Length - 1; i >= 0; i--) {
      var c = chunkMap[i];
      yield return (i, c, c.lastVisibility, c.dirty, c.initialised);
    }
  }

  /// <summary>
  /// Makes the instance
  /// </summary>
  /// <param name="info"> chunk to instance as game object </param>
  bool CreateInstance((int index, Chunk chunk) info) {
    if (!info.chunk.tile.Visible) {
      // cannot instance when out of view
      return false;
    }

    info.chunk.CreateChunkInstance(info.index, transform);
    // mark the chunk as initialised
    info.chunk.initialised = true;
    // mark chunk as dirty so the new instance will update
    info.chunk.dirty = true;
    return true;
  }

  public Chunk GetChunk(int index) {
    return chunkMap[index];
  }
}
}