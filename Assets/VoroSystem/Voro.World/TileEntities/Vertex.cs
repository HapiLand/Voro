using System;
using UnityEngine;

namespace VoroSystem.Voro.World.TileEntities {
[Serializable]
public class Vertex {
  #region Serialized Fields

  public Vector3 position;

  #endregion

  public Vertex(Vector3 pos) {
    position = pos;
  }
}
}