using System;
using UnityEngine;

namespace VoroSystem.Voro.World.ChunkStructure {
[Serializable]
public class CVertex {
  #region Serialized Fields

  public Vector3 position;

  #endregion

  public CVertex(Vector3 pos) {
    position = pos;
  }
}
}