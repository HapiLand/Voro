using UnityEngine;
using VoroSystem.VoroWorldGeneration.CubicChunks.World.Core;
using VoroSystem.VoroWorldGeneration.Map;

namespace VoroSystem.VoroWorldGeneration.CubicChunks.Cubes.Core {
[ExecuteAlways]
[RequireComponent(typeof(WorldGenTilemap), typeof(WorldGenInstancer))]
public abstract class BaseCube : MonoBehaviour {
  protected WorldGenTilemap MapGenerator;
  protected WorldGenInstancer TileInstancer;
  protected Tilemap<Tile> Tilemap;
  protected virtual float GizmoBaseSize => WorldSettings.GridSize;
  protected virtual bool IsPlayerInside => false;
  protected virtual bool NeighborHasPlayer => false;

  #region Event Functions
  protected virtual void Awake() {
    TileInstancer = GetComponent<WorldGenInstancer>();
    MapGenerator = GetComponent<WorldGenTilemap>();
    TileInstancer.Init(MapGenerator);
  }

  protected virtual void OnDrawGizmos() {
    GetVisualState(out var color, out var size);
    Gizmos.color = color;
    Gizmos.DrawWireCube(transform.position, Vector3.one * size);
  }
  #endregion

  public void GetVisualState(out Color color, out float size) {
    if (IsPlayerInside) {
      size = GizmoBaseSize;
      color = Color.green;
      return;
    }

    if (NeighborHasPlayer) {
      size = GizmoBaseSize * 0.8f;
      color = Color.blue;
      return;
    }

    size = GizmoBaseSize * 0.25f;
    color = Color.red;
  }
}
}