using UnityEngine;
using VoroSystem.VoroWorldGeneration.Map;

namespace VoroSystem.VoroWorldGeneration {
/// <summary>
/// instantiates tile GameObjects into the world upon a tile being created
/// </summary>
[ExecuteAlways]
public class WorldGenInstancer : MonoBehaviour {
  WorldGenTilemap _tilemap;

  #region Event Functions
  void OnDisable() {
    if (_tilemap != null) {
      _tilemap.OnNewTile -= HandleNewTileCreated;
    }
  }
  #endregion

  public void Init(WorldGenTilemap tilemap) {
    if (_tilemap != null) {
      _tilemap.OnNewTile -= HandleNewTileCreated;
    }

    _tilemap = tilemap;
    if (_tilemap != null) {
      _tilemap.OnNewTile += HandleNewTileCreated;
    }
  }

  /// <summary>
  /// instantiate the entity so its GameObject and components exist
  /// </summary>
  /// <param name="tile"> </param>
  void HandleNewTileCreated(Tile tile) {
    tile.CreateEntity(transform);
    tile.Update();
  }

  public static void ClearGrid(Tilemap<Tile> grid) {
    if (grid == null) {
      return;
    }

    grid.ForEach(tile => {
      if (tile.Entity) {
        DestroyImmediate(tile.Entity);
      }
    });
    Debug.Log("Grid cleared");
  }
}
}