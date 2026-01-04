using System;
using UnityEditor;
using UnityEngine;

namespace Voro.Internal.World.GameWorldMap.Primitives {
/// <summary>
/// horizontal line of infinite length, along the line is a basic area
/// infinitely horizontal line within the world, GridTiles are created around this
/// TiledLine acts as a set of points at a world position
/// tiles exist around a radius of each point
/// </summary>
public class InfiniteLinePrim : PrimitiveObject {
  #region LineDirection enum
  [Serializable]
  public enum LineDirection {
    XAxis,
    ZAxis
  }
  #endregion

  #region Serialized Fields
  public LineDirection direction = LineDirection.XAxis;
  [Min(0.1f)] [SerializeField] float width = 1f;
  #endregion

  Vector3 Axis => direction switch
  {
    LineDirection.XAxis => Vector3.right,
    LineDirection.ZAxis => Vector3.forward,
    _ => Vector3.right
  };

  #region Event Functions
  protected override void OnDrawGizmos() {
    Gizmos.color = Color.white;
    Line(transform.position, direction);
    return;

    void Line(Vector3 origin, LineDirection dir) {
      const float length = 10000f;
      var a = origin - Axis * length;
      var b = origin + Axis * length;
      Gizmos.DrawLine(a, b);
    }
  }
  #endregion

  public override bool IsPointInside(Vector3 p) {
    switch (direction) {
    case LineDirection.XAxis:
      if (Mathf.Abs(p.z - transform.position.z) > width) {
        return false;
      }

      break;
    case LineDirection.ZAxis:
      if (Mathf.Abs(p.x - transform.position.x) > width) {
        return false;
      }

      break;
    }

    return false;
  }

  public override Bounds Bounds() => throw new NotImplementedException();

  [MenuItem("GameObject/Voro/Internal/World/Primitives/Infinite Line", false, 999)]
  public static void Create() {
    GameObjectUtility.CreateWithComponent<InfiniteLinePrim>();
  }
}
}