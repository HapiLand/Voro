using UnityEngine;

namespace Voro.Internal.World.GameWorldMap.Primitives {
/// <summary>
/// a primitive for where the areas of the world can be
/// </summary>
[ExecuteAlways]
public abstract class PrimitiveObject : MonoBehaviour {
  #region Event Functions
  protected abstract void OnDrawGizmos();
  #endregion

  public abstract bool IsPointInside(Vector3 p);
  public abstract Bounds Bounds();
}
}