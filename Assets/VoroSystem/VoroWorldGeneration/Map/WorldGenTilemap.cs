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

  public static event TileAction OnNewTile = delegate { };

  public void GenerateWorldGrid(TilemapReady onComplete) {
    Debug.Log($"Creating new [{WorldGenMapSettings.Width} x {WorldGenMapSettings.Height}] TileMap");
    _tilemap = new Tilemap<Tile>(
      WorldGenTileSettings.TileSize,
      WorldGenMapSettings.Width,
      WorldGenMapSettings.Height,
      (index, pos) => {
        var tile = new Tile(index, pos);
        OnNewTile?.Invoke(tile);
        return tile;
      });

    Debug.Log("Starting Coroutine");
    StartCoroutine(Generate(onComplete));
  }

  IEnumerator Generate(TilemapReady onComplete) {
    yield return _tilemap.CreateMapAsync(WorldGenMapSettings.GenerateTilesPerFrame);
    onComplete?.Invoke(_tilemap);
    // Debug.Log($"Tilemap generated [{WorldGenMapSettings.Width} x {WorldGenMapSettings.Height}] grid");
    //onComplete?.Invoke(grid);
  }
}
}