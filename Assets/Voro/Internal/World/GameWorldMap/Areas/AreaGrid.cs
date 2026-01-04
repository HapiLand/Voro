using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Voro.Internal.World.GameWorldMap.Primitives;

namespace Voro.Internal.World.GameWorldMap.Areas {
/// <summary>
/// collection of BasicAreas found within a Primitive
/// </summary>
[ExecuteAlways]
public class AreaGrid : MonoBehaviour {
  #region Serialized Fields
  [Min(0.1f)] [SerializeField] float gridSize = 1f;
  [SerializeField] List<BasicArea> areas = new();

  /// <summary>
  /// primitive to find the area inside
  /// </summary>
  [SerializeField] PrimitiveObject prim;
  #endregion

  #region Event Functions
  void OnDrawGizmos() {
    if (!prim) {
      return;
    }

    foreach (var area in areas) {
      Gizmos.color = Color.gray3;
      Circle(area.position, area.radius);

      Gizmos.color = Color.gray4;
      var bounds = Bounds();
      Gizmos.DrawWireCube(bounds.center, bounds.size);
      continue;

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
  }
  #endregion


  public void SetPrimitive(PrimitiveObject worldSpace) {
    prim = worldSpace;
    areas.Clear();
    CreateGrid();
  }

  void CreateGrid() {
    var bounds = prim.Bounds();
    var xSize = Mathf.Max(1, Mathf.CeilToInt(bounds.size.x));
    var zSize = Mathf.Max(1, Mathf.CeilToInt(bounds.size.z));

    var xCount = Mathf.CeilToInt(xSize / gridSize);
    var zCount = Mathf.CeilToInt(zSize / gridSize);

    var startX = bounds.center.x - xCount * gridSize / 2f + gridSize / 2f;
    var startZ = bounds.center.z - zCount * gridSize / 2f + gridSize / 2f;

    for (var x = 0; x < xCount; x++) {
      for (var z = 0; z < zCount; z++) {
        var pos = new Vector3(startX + x * gridSize, 0, startZ + z * gridSize);

        // allow the area if it is within the primitive
        if (!prim.IsPointInside(pos)) {
          continue;
        }

        var area = new BasicArea(pos, gridSize);
        areas.Add(area);
      }
    }
  }

  public Bounds Bounds() {
    var minX = 100f;
    var maxX = 1f;
    var minZ = 100f;
    var maxZ = 1f;

    foreach (var area in areas) {
      var pos = area.position;
      minX = Mathf.Min(minX, pos.x - area.radius);
      maxX = Mathf.Max(maxX, pos.x + area.radius);
      minZ = Mathf.Min(minZ, pos.z - area.radius);
      maxZ = Mathf.Max(maxZ, pos.z + area.radius);
    }

    var center = new Vector3((minX + maxX) / 2f, 0, (minZ + maxZ) / 2f);
    var size = new Vector3(maxX - minX, 0, maxZ - minZ);
    return new Bounds(center, size);
  }

  public bool IsPointInside(Vector3 p) {
    return areas.Where(area => area.IsPointInside(p)).Any();
  }
}
}