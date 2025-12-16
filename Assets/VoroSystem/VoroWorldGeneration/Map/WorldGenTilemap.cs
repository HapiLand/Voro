using UnityEngine;

namespace VoroSystem.VoroWorldGeneration.Map {
[ExecuteAlways]
public class WorldGenTilemap : MonoBehaviour {
  #region Delegates
  public delegate void TileAction(Tile tile);
  #endregion

  public static Tilemap<Tile> GenerateWorldGrid() {
    var grid = new Tilemap<Tile>(
      WorldGenTileSettings.TileSize,
      WorldGenMapSettings.Width,
      WorldGenMapSettings.Height,
      (index, pos) => {
        // create new tile instance
        var tile = new Tile(index, pos);
        OnNewTile?.Invoke(tile);
        return tile;
      });
    Debug.Log($"Tilemap generated [{WorldGenMapSettings.Width} x {WorldGenMapSettings.Height}] grid");
    return grid;
  }

  public static event TileAction OnNewTile = delegate { Debug.Log("[TileAction] Created Tile"); };
}
}