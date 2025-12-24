using System;
using System.Collections;
using UnityEngine;

namespace VoroSystem.VoroWorldGeneration.Map {
[Serializable]
public abstract class BaseTilemap<T> where T : Tile {
  #region Serialized Fields
  [SerializeField] public T[] map;
  public float TileSize => WorldGenTileSettings.TileSize;
  /// <summary>
  /// the base size of the tilemap, tiles are placed within these dimensions
  /// </summary>
  [SerializeField] public Vector3Int baseDimensions;
  #endregion
  public Vector3 mapOrigin;
  protected Func<(int tileIndex, Vector3 worldOrigin), T> Factory;

  protected BaseTilemap(Vector3Int boundsSize, Vector3 originPosition, Func<(int tileIndex, Vector3 worldOrigin), T> factory) {
    mapOrigin = originPosition;
    baseDimensions = boundsSize;
    Factory = factory;
  }

  public T this[int index] {
    get => map[index];
    set => map[index] = value;
  }

  public T this[int x, int z] {
    get
    {
      var tilesX = Mathf.Max(1, Mathf.RoundToInt(baseDimensions.x / TileSize));
      var tilesZ = Mathf.Max(1, Mathf.RoundToInt(baseDimensions.z / TileSize));

      if (x < 0 || x >= tilesX || z < 0 || z >= tilesZ) {
        return null;
      }

      var index = GetIndex(x, z, tilesX);
      return map[index];
    }
    set
    {
      var tilesX = Mathf.Max(1, Mathf.RoundToInt(baseDimensions.x / TileSize));
      var tilesZ = Mathf.Max(1, Mathf.RoundToInt(baseDimensions.z / TileSize));

      if (x < 0 || x >= tilesX || z < 0 || z >= tilesZ) {
        return;
      }

      var index = GetIndex(x, z, tilesX);
      map[index] = value;
    }
  }

  // public abstract void CreateMap();
  public abstract IEnumerator CreateMapAsync(int tilesPerFrame = 100);

  public T GetAtPosition(Vector3 pos) {
    var x = (int)(pos.x / TileSize);
    var z = (int)(pos.z / TileSize);
    return this[x, z];
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