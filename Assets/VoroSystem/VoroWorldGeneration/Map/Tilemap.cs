using System;
using System.Collections;
using UnityEngine;

namespace VoroSystem.VoroWorldGeneration.Map {
public class Tilemap<T> : BaseTilemap<T> where T : Tile {
  readonly int _tilesX;
  readonly int _tilesZ;

  public Tilemap(float tileSize, int mapSizeX, int mapSizeZ, Func<int, Vector2, T> factory)
    : base(tileSize, mapSizeX, mapSizeZ, factory) {
    _tilesX = Mathf.Max(1, Mathf.CeilToInt(mapSizeX / tileSize));
    _tilesZ = Mathf.Max(1, Mathf.CeilToInt(mapSizeZ / tileSize));
    var totalTiles = _tilesX * _tilesZ;
    Debug.Log("Creating initial map");
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
      var x = i % _tilesX;
      var z = i / _tilesX;

      var worldPos = new Vector2(x * tileSize, z * tileSize);
      map[i] = Factory(i, worldPos);

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

    /*for (var z = 0; z < tilesZ; z++) {
      for (var x = 0; x < tilesX; x++) {
        var index = GetIndex(x, z, tilesX);
        var worldPos = new Vector2(x * tileSize, z * tileSize);
        map[index] = Factory(index, worldPos);
        createdThisFrame++;
        if (createdThisFrame >= tilesPerFrame)
        {
          createdThisFrame = 0;
          yield return null;
        }
      }
    }*/
  }

  /*public override void CreateMap() {
    var tilesX = Mathf.Max(1, Mathf.RoundToInt(mapSizeX / tileSize));
    var tilesZ = Mathf.Max(1, Mathf.RoundToInt(mapSizeZ / tileSize));

    map = new T[tilesX * tilesZ];

    for (var z = 0; z < tilesZ; z++) {
      for (var x = 0; x < tilesX; x++) {
        var index = GetIndex(x, z, tilesX);
        var worldPos = new Vector2(x * tileSize, z * tileSize);
        map[index] = Factory(index, worldPos);
      }
    }
  }*/
}
}