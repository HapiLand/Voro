using System;
using UnityEngine;
using VoroSystem.VoroWorldGeneration.Map;

namespace VoroSystem.VoroWorldGeneration {
[ExecuteAlways]
public class WorldGenInstancer : MonoBehaviour {
  public static WorldGenInstancer Instance { get; private set; }
  #region Event Functions
  void Awake() {
    if (Instance != null)
    {
      Destroy(gameObject);
      return;
    }
    Instance = this;
    
    WorldGenTilemap.OnNewTile += HandleNewTileCreated;
  }

  void OnDisable() {
    WorldGenTilemap.OnNewTile -= HandleNewTileCreated;
  }
  #endregion

  /// <summary>
  /// instantiate the entity so its GameObject and components exist
  /// </summary>
  /// <param name="tile"> </param>
  void HandleNewTileCreated(Tile tile) {
    tile.CreateEntity(transform);
    // Debug.Log("Tile instanced into scene");
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