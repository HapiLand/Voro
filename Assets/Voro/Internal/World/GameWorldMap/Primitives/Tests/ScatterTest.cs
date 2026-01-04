using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Voro.Internal.World.GameWorldMap.Primitives.Tests {
/// <summary>
/// randomly scatter points for testing a prim
/// </summary>
[ExecuteAlways]
public class ScatterTest : MonoBehaviour {
  #region Serialized Fields
  [SerializeField] PrimitiveObject prim;
  [SerializeField] List<Vector3> points = new();
  [Min(1f)] [SerializeField] float waitDuration = 1f;
  [Min(1f)] [SerializeField] float radius = 2f;

  [SerializeField] float stopwatch;
  #endregion

  #region Event Functions
  void Update() {
    if (!prim || !(Time.realtimeSinceStartup >= stopwatch)) {
      return;
    }

    stopwatch = Time.realtimeSinceStartup + waitDuration;
    Scatter();
  }

  void OnDrawGizmos() {
    if (!prim) {
      return;
    }

    foreach (var point in points) {
      Gizmos.color = prim.IsPointInside(point) ? Color.green : Color.red;
      Gizmos.DrawSphere(point, 0.05f);
    }
  }
  #endregion

  void Scatter(int pointCount = 100) {
    points.Clear();
    for (var i = 0; i < pointCount; i++) {
      var pos2D = Random.insideUnitCircle * radius;
      var pos = new Vector3(pos2D.x, 0f, pos2D.y);
      pos += transform.position;

      points.Add(pos);
    }
  }

  [MenuItem("GameObject/Voro/Internal/World/Primitives/Tests/Scatter", false, 999)]
  public static void Create() {
    GameObjectUtility.CreateWithComponent<ScatterTest>();
  }
}
}