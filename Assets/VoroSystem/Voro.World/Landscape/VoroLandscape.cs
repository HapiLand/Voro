using UnityEngine;
using VoroSystem.Voro.World.Landscape.Map;

namespace VoroSystem.Voro.World.Landscape {
/// <summary>
/// Defines the space in where the environment exists
/// </summary>
public class VoroLandscape : MonoBehaviour {
  #region Serialized Fields

  [SerializeField] WorldBoundary worldBoundary;
  [SerializeField] WorldGrid worldGrid;
  [SerializeField] WorldTilemap worldTilemap;

  #endregion

  public int MapXSize => worldGrid.Dimensions.xSize;
  public int MapZSize => worldGrid.Dimensions.zSize;
  public float TileSize => worldGrid.Dimensions.gridSize;

  #region Event Functions

  void Awake() {
    worldBoundary ??= new WorldBoundary();
    worldGrid ??= new WorldGrid();
    worldTilemap ??= GetComponent<WorldTilemap>();

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