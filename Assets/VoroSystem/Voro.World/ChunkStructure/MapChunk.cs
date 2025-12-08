using System;
using UnityEngine;
using VoroSystem.Voro.World.ChunkStructure.Interfaces;

namespace VoroSystem.Voro.World.ChunkStructure {
[Serializable]
public class MapChunk : IMapChunk {
  #region Serialized Fields

  [SerializeField] int index;
  [SerializeField] float size;

  #endregion

  public MapChunk(int index, float size) {
    this.index = index;
    this.size = size;
  }

  #region IMapChunk Members

  public int MapIndex => index;
  public float ChunkSize => size;

  #endregion
}
}