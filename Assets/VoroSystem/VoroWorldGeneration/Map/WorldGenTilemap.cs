using System.Collections;
using UnityEngine;
using Voro.Internal.World;

namespace VoroSystem.VoroWorldGeneration.Map {
/// <summary>
/// responsible for generating a Tilemap, async creates tiles, detects when a tile is created
/// </summary>
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

  public void GenerateWorldGrid(Chunk cubeBoundingBox, TilemapReady onComplete) {
    var originPosition = cubeBoundingBox.WorldOriginPosition;
    Debug.Log($"Creating [{cubeBoundingBox.BoundSize}] Tilemap at {originPosition}");

    _tilemap = new Tilemap<Tile>(
      cubeBoundingBox.BoundSize,
      originPosition,
      tuple => {
        var tile = new Tile(tuple.tileIndex, tuple.worldOrigin);
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

  /// <summary>
  /// determine if a tilemap is allowed to be generated
  /// </summary>
  /// <param name="allowGeneration"> </param>
  public void Check(out bool allowGeneration) {
    allowGeneration = true;
    // todo implement check to ensure the tilemap is allowed to be created
    //  player must be inside this, or in neighbor in order to generate
  }
}
}