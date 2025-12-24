using UnityEngine;

namespace VoroSystem.VoroWorldGeneration.CubicChunks.Player {
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

  public static Vector3 GridToWorld(Vector3Int gridCoord, float cubeSize) {
    return (Vector3)gridCoord * cubeSize + Vector3.one * (cubeSize * 0.5f);
  }
}
}