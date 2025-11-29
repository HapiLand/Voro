using System;
using UnityEngine;
using VoroSystem.Voro.Utilities;

namespace VoroSystem.Voro.World {
[Serializable]
public class Tilemap<T> {
  #region Serialized Fields

  [SerializeField] public T[] map;
  [SerializeField] public float tileSize;
  [SerializeField] public int mapSizeX;
  [SerializeField] public int mapSizeZ;

  #endregion

  Func<int, Vector2, T> factory;

  public Tilemap(float tileSize, int mapSizeX, int mapSizeZ, Func<int, Vector2, T> factory) {
    this.tileSize = tileSize;
    this.mapSizeX = mapSizeX;
    this.mapSizeZ = mapSizeZ;
    this.factory = factory;
    CreateMap();
  }

  public T this[int index] => map[index];

  public void CreateMap() {
    var tilesX = Mathf.Max(1, Mathf.RoundToInt(mapSizeX / tileSize));
    var tilesZ = Mathf.Max(1, Mathf.RoundToInt(mapSizeZ / tileSize));

    map = new T[tilesX * tilesZ];

    for (var z = 0; z < tilesZ; z++) {
      for (var x = 0; x < tilesX; x++) {
        var index = HelperUtility.GetIndex(x, z, tilesX);
        var worldPos = new Vector2(x * tileSize, z * tileSize);
        map[index] = factory(index, worldPos);
      }
    }
  }

  public T GetAtPosition(Vector2 pos) {
    var x = (int)(pos.x / tileSize);
    var z = (int)(pos.y / tileSize);
    var tilesX = Mathf.Max(1, Mathf.RoundToInt(mapSizeX / tileSize));
    var tilesZ = Mathf.Max(1, Mathf.RoundToInt(mapSizeZ / tileSize));
    if (x < 0 || x >= tilesX || z < 0 || z >= tilesZ) {
      return default;
    }

    var index = HelperUtility.GetIndex(x, z, tilesX);
    return map[index];
  }

  public void ForEach(Action<T> action) {
    foreach (var t in map) {
      action(t);
    }
  }
}
}