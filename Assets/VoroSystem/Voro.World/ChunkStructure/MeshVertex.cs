using System;
using UnityEngine;

namespace VoroSystem.Voro.World.ChunkStructure {
[Serializable]
public class MeshVertex {
  #region Serialized Fields

  public float height;
  public Vector3 position;

  #endregion

  public MeshVertex(Vector3 pos) {
    position = pos;
    height = 0;
  }

  public Vector3 WorldPosition => new(position.x, height, position.z);

  public struct PointData {
    public Vector3 Position;
  }
}
}