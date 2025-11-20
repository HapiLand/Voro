using System;
using UnityEngine;

namespace VoroSystem.Voro.World.Voro.Terrain.Ground.Chunks.Geometry {
[Serializable]
public class QuadVertex {
  #region Serialized Fields

  public Vector3 position;

  #endregion

  public QuadVertex(Vector3 pos) {
    position = pos;
  }
}
}