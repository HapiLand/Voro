using System;
using UnityEngine;
using VoroSystem.Util;

namespace VoroSystem.Voro.World.Landscape.Map {
/// <summary>
/// Tile array
/// </summary>
public class WorldTilemap : MonoBehaviour {
  #region Serialized Fields

  [SerializeField] WorldGrid worldGrid;
  [SerializeField] Tile[] tiles;

  #endregion

  Vector2Int _lastDimensions;
  bool DimensionsChanged => _lastDimensions.x != SizeX || _lastDimensions.y != SizeZ;

  int SizeX => worldGrid?.Dimensions.xSize ?? 1;
  int SizeZ => worldGrid?.Dimensions.zSize ?? 1;

  #region Event Functions

  void Update() {
    if (DimensionsChanged) {
      RegenerateMap();
    }

    ForEach(tile => { tile.Update(); });
  }

  #endregion

  public void Initialize(WorldGrid grid) {
    worldGrid = grid;
    InitTilemap();
  }

  void RegenerateMap() {
    InitTilemap();
  }

  void InitTilemap() {
    tiles = new Tile[SizeX * SizeZ];
    _lastDimensions = new Vector2Int(SizeX, SizeZ);
    for (var z = 0; z < SizeZ; z++) {
      for (var x = 0; x < SizeX; x++) {
        CreateTile(x, z);
      }
    }
  }

  void CreateTile(int x, int z) {
    var index = HelperUtility.GetIndex(x, z, SizeX);
    tiles[index] = new Tile(index, new Vector2(x, z), worldGrid.GridSize);
  }

  public void ForEach(Action<Tile> action) {
    foreach (var t in tiles) {
      action(t);
    }
  }

  public Tile GetTile(int index) {
    return tiles[index];
  }
}
}