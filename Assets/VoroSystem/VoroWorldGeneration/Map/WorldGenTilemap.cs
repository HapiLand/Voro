using System.Collections;
using UnityEngine;

namespace VoroSystem.VoroWorldGeneration.Map {
[ExecuteAlways]
public class WorldGenTilemap : MonoBehaviour {
  #region Delegates
  public delegate void TileAction(Tile tile);

  public delegate void TilemapReady(Tilemap<Tile> tilemap);
  #endregion

  Tilemap<Tile> _tilemap;

  #region Event Functions
  void Update() {
    _tilemap?.ForEach(tile => {
      if (tile == null) {
        return;
      }

      if (tile.Entity != null) {
        tile.Update();
      }
    });
  }
  #endregion

  public event TileAction OnNewTile = delegate { };

  public void GenerateWorldGrid(Vector3Int dimensions, TilemapReady onComplete) {
    Debug.Log($"Creating new [{dimensions.x} x {dimensions.z}] TileMap");

    _tilemap = new Tilemap<Tile>(
      WorldGenTileSettings.TileSize,
      dimensions.x,
      dimensions.z,
      (index, pos) => {
        var tile = new Tile(index, pos);
        OnNewTile?.Invoke(tile);
        return tile;
      });

    Debug.Log("Starting Coroutine");
    StartCoroutine(GenerateAsync(onComplete));
  }

  IEnumerator GenerateAsync(TilemapReady onComplete) {
    yield return _tilemap.CreateMapAsync(WorldGenMapSettings.GenerateTilesPerFrame);
    onComplete?.Invoke(_tilemap);
  }
}
}