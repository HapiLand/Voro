using System;
using UnityEngine;

namespace Voro.Internal.World.GameWorldMap.Areas {
/// <summary>
/// an area in the world where objects exist within.
/// </summary>
[Serializable]
public struct BasicArea {
  public Vector3 position;
  public float radius;

  public BasicArea(Vector3 position, float radius) {
    this.position = position;
    this.radius = radius;
  }

  public bool IsPointInside(Vector3 p) => Vector3.Distance(p, position) <= radius;
}
}