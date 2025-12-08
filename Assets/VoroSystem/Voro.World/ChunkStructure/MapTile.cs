using System;
using UnityEngine;
using VoroSystem.Voro.World.ChunkStructure.Interfaces;

namespace VoroSystem.Voro.World.ChunkStructure {
[Serializable]
public class MapTile : IMapTile {
  #region Serialized Fields

  [SerializeField] int index;
  [SerializeField] float size;

  #endregion

  public MapTile(int index, float size) {
    this.index = index;
    this.size = size;
  }

  #region IMapTile Members

  public int Index => index;
  public float Size => size;

  #endregion
}
}