using UnityEngine;
using Voro.Internal.World;
using Voro.Internal.World.GridTiles;
using VoroSystem.VoroWorldGeneration.CubicChunks.World.Core;

namespace VoroSystem.VoroWorldGeneration.CubicChunks.Player.Core {
public static class PlayerLocator {
  public static PlayerPoint GetOrCreatePlayer() {
    var player = Object.FindFirstObjectByType<PlayerPoint>();
    if (player != null) {
      return player;
    }

    var playerObj = new GameObject("PlayerPoint");
    return playerObj.AddComponent<PlayerPoint>();
  }

  public static Vector3Int GetPlayerGridCoordinate(PlayerPoint player, float cubeSize) {
    if (player == null) {
      return Vector3Int.zero;
    }

    var position = player.transform.position;

    return new Vector3Int(
      Mathf.FloorToInt(position.x / cubeSize),
      Mathf.FloorToInt(position.y / cubeSize),
      Mathf.FloorToInt(position.z / cubeSize)
    );
  }

  /// <summary>
  /// returns the position where the bottom-left corner of a cube is located at the coordinate
  /// </summary>
  /// <param name="gridCoord"> origin point of a cube (bottom-left) </param>
  /// <returns> </returns>
  public static Vector3 GridToWorld(Vector3Int gridCoord) {
    return (Vector3)gridCoord * GridTileSettings.GridTileSize + Vector3.one * (GridTileSettings.GridTileSize * 0.5f);
  }
}
}