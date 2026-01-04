using UnityEditor;
using UnityEngine;

namespace Voro.Internal.World.GameWorldMap.Primitives {
/// <summary>
/// circular shaped area in the world
/// </summary>
public class CirclePrim : PrimitiveObject {
  #region Serialized Fields
  [Min(0.1f)] [SerializeField] float radius = 5f;
  #endregion

  #region Event Functions
  protected override void OnDrawGizmos() {
    Gizmos.color = Color.white;
    Circle(transform.position, radius);
    return;

    void Circle(Vector3 origin, float r, int segments = 32) {
      var step = 2f * Mathf.PI / segments;
      var prevPoint = origin + new Vector3(r, 0f, 0f);
      for (var i = 1; i <= segments; i++) {
        var theta = step * i;
        var nextPoint = origin + new Vector3(Mathf.Cos(theta) * r, 0f, Mathf.Sin(theta) * r);
        Gizmos.DrawLine(prevPoint, nextPoint);
        prevPoint = nextPoint;
      }
    }
  }
  #endregion

  public override bool IsPointInside(Vector3 p) => Vector3.Distance(p, transform.position) <= radius;

  public override Bounds Bounds() {
    var center = transform.position;
    var size = new Vector3(radius * 2f, 0, radius * 2f);
    return new Bounds(center, size);
  }

  [MenuItem("GameObject/Voro/Internal/World/Primitives/Circle", false, 999)]
  public static void Create() {
    GameObjectUtility.CreateWithComponent<CirclePrim>();
  }
}
}