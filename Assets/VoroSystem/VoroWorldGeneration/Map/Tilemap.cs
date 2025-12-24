using System;
using System.Collections;
using UnityEngine;

namespace VoroSystem.VoroWorldGeneration.Map {
public class Tilemap<T> : BaseTilemap<T> where T : Tile {
  public Vector3Int mapDimensions => new Vector3Int(
    Mathf.Max(1, Mathf.CeilToInt(baseDimensions.x / TileSize)),
    Mathf.Max(1, Mathf.CeilToInt(baseDimensions.y / TileSize)),
    Mathf.Max(1, Mathf.CeilToInt(baseDimensions.z / TileSize)));
  public Tilemap(Vector3Int boundsSize, Vector3 boundsWorldOrigin, Func<(int tileIndex, Vector3 worldOrigin), T> factory) : base(boundsSize, boundsWorldOrigin, factory) {
    var totalTiles = mapDimensions.x * mapDimensions.z;
    map = new T[totalTiles];
  }

  /// <summary>
  /// called by a Coroutine, creates tiles in the tilemap
  /// </summary>
  /// <param name="tilesPerFrame"> number of tiles that are allowed to be created </param>
  /// <returns> </returns>
  public override IEnumerator CreateMapAsync(int tilesPerFrame = 100) {
    tilesPerFrame = Mathf.Max(1, tilesPerFrame);
    var createdThisFrame = 0;
    var totalCreated = 0;
    var batchIndex = 0;

    for (var i = 0; i < map.Length; i++) {
      var x = i % mapDimensions.x;
      var z = i / mapDimensions.z;

      var index = i;
      var worldPosition = new Vector3(
        x * TileSize + mapOrigin.x, 
        mapOrigin.y, 
        z * TileSize + mapOrigin.z);
      var tuple = (index, worldPosition);
      map[i] = Factory(tuple);

      createdThisFrame++;
      totalCreated++;

      if (createdThisFrame >= tilesPerFrame) {
        batchIndex++;
        Debug.Log($"batch {batchIndex}: {totalCreated}/{map.Length} tiles created");
        createdThisFrame = 0;
        yield return null;
      }
    }

    Debug.Log("tilemap CreateMapAsync completed");
  }
}
}