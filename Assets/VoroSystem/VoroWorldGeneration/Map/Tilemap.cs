using System;
using UnityEngine;

namespace VoroSystem.VoroWorldGeneration.Map {
public class Tilemap<T> : BaseTilemap<T> where T : Tile {
  public Tilemap(float tileSize, int mapSizeX, int mapSizeZ, Func<int, Vector2, T> factory)
    : base(tileSize, mapSizeX, mapSizeZ, factory) { }

  public override void CreateMap() {
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
  }
}
}