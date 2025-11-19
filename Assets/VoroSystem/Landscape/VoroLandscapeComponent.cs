using UnityEngine;
using VoroSystem.Landscape.WorldBoundarySystem;
using VoroSystem.Landscape.WorldGridSystem;
using VoroSystem.Landscape.WorldMapSystem;

namespace VoroSystem.Landscape {
/// <summary>
/// Defines the space in where the environment exists
/// </summary>
[ExecuteAlways]
[RequireComponent(typeof(WorldBoundaryComponent))]
[RequireComponent(typeof(WorldGridComponent))]
[RequireComponent(typeof(WorldTilemapComponent))]
public class VoroLandscapeComponent : MonoBehaviour {
  #region Serialized Fields

  [SerializeField] WorldBoundaryComponent worldBoundary;
  [SerializeField] WorldGridComponent worldGrid;
  [SerializeField] WorldTilemapComponent worldTilemap;

  #endregion

  public int MapXSize => worldGrid.Dimensions.xSize;
  public int MapZSize => worldGrid.Dimensions.zSize;
  public float TileSize => worldGrid.Dimensions.gridSize;

  #region Event Functions

  void Awake() {
    worldBoundary ??= GetComponent<WorldBoundaryComponent>();
    worldGrid ??= GetComponent<WorldGridComponent>();
    worldTilemap ??= GetComponent<WorldTilemapComponent>();

    name = "VoroLandscape";

    // init WorldGrid so it gains the bounding box size
    worldGrid.Initialize(worldBoundary);
    // init WorldTilemap so the array can be created
    worldTilemap.Initialize(worldGrid);
  }

  #endregion

  public Tile GetTile(int index) {
    return worldTilemap.GetTile(index);
  }
}
}