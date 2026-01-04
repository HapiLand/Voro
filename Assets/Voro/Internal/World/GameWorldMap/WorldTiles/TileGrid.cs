using System.Collections.Generic;
using UnityEngine;
using Voro.Internal.World.GameWorldMap.Areas;

namespace Voro.Internal.World.GameWorldMap.WorldTiles {
/// <summary>
/// collection of WorldTiles within the AreaGrid for the world
/// </summary>
[ExecuteAlways]
public class TileGrid : MonoBehaviour {
  #region Serialized Fields
  [SerializeField] AreaGrid area;
  [SerializeField] List<WorldTile> tiles = new();
  #endregion

  static float GridSize => TileSettings.TileSize;

  public void SetArea(AreaGrid worldArea) {
    area = worldArea;
    tiles.Clear();
    while (transform.childCount > 0) {
      DestroyImmediate(transform.GetChild(0).gameObject);
    }

    CreateGrid();
  }

  void CreateGrid() {
    var bounds = area.Bounds();
    var xSize = Mathf.Max(1, Mathf.CeilToInt(bounds.size.x));
    var zSize = Mathf.Max(1, Mathf.CeilToInt(bounds.size.z));

    var xCount = Mathf.CeilToInt(xSize / GridSize);
    var zCount = Mathf.CeilToInt(zSize / GridSize);

    var startX = bounds.center.x - xCount * GridSize / 2f + GridSize / 2f;
    var startZ = bounds.center.z - zCount * GridSize / 2f + GridSize / 2f;

    for (var x = 0; x < xCount; x++) {
      for (var z = 0; z < zCount; z++) {
        var posX = Mathf.CeilToInt(startX + x * GridSize);
        var posZ = Mathf.CeilToInt(startZ + z * GridSize);

        // allow the tile if it is within the area
        if (!area.IsPointInside(new Vector3(posX, 0, posZ))) {
          continue;
        }

        var tile = GameObjectUtility.CreateWithComponent<WorldTile>(
          $"{posX},{posZ}",
          gameObject.transform,
          new Vector3(posX, 0, posZ));
        tile.coordinate = new Vector3Int(posX, 0, posZ);


        tiles.Add(tile);
      }
    }
  }
}
}