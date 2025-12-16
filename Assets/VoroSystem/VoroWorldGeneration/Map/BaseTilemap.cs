using System;
using UnityEngine;

namespace VoroSystem.VoroWorldGeneration.Map {
[Serializable]
public abstract class BaseTilemap<T> where T : Tile {
  #region Serialized Fields
  [SerializeField] public T[] map;
  [SerializeField] public float tileSize;
  [SerializeField] public int mapSizeX;
  [SerializeField] public int mapSizeZ;
  #endregion

  protected Func<int, Vector2, T> Factory;

  protected BaseTilemap(float tileSize, int mapSizeX, int mapSizeZ, Func<int, Vector2, T> factory) {
    this.tileSize = tileSize;
    this.mapSizeX = mapSizeX;
    this.mapSizeZ = mapSizeZ;
    Factory = factory;
    CreateMap();
  }

  public T this[int index] {
    get => map[index];
    set => map[index] = value;
  }

  public T this[int x, int z] {
    get
    {
      var tilesX = Mathf.Max(1, Mathf.RoundToInt(mapSizeX / tileSize));
      var tilesZ = Mathf.Max(1, Mathf.RoundToInt(mapSizeZ / tileSize));

      if (x < 0 || x >= tilesX || z < 0 || z >= tilesZ) {
        return null;
      }

      var index = GetIndex(x, z, tilesX);
      return map[index];
    }
    set
    {
      var tilesX = Mathf.Max(1, Mathf.RoundToInt(mapSizeX / tileSize));
      var tilesZ = Mathf.Max(1, Mathf.RoundToInt(mapSizeZ / tileSize));

      if (x < 0 || x >= tilesX || z < 0 || z >= tilesZ) {
        return;
      }

      var index = GetIndex(x, z, tilesX);
      map[index] = value;
    }
  }


  public abstract void CreateMap();

  public T GetAtPosition(Vector2 pos) {
    var x = (int)(pos.x / tileSize);
    var z = (int)(pos.y / tileSize);
    return this[x, z]; // Use the new indexer
  }

  public int GetIndex(int x, int z, int sizeX) {
    return z * sizeX + x;
  }

  public void ForEach(Action<T> action) {
    foreach (var t in map) {
      action(t);
    }
  }
}
}