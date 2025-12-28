using System.Collections.Generic;

namespace Voro.Internal.World {
/// <summary>
/// monitors every chunk that exists within the world
/// </summary>
public static class ChunkMonitor {
  static readonly HashSet<Chunk> chunks = new();
  public static IReadOnlyCollection<Chunk> Chunks => chunks;

  /*static bool DoesChunkExist(Vector3Int gridCoord) {
    var exists = false;
    foreach (var c in chunks.Where(c => c.GridCoord == gridCoord)) {
      exists = true;
    }
    return exists;
  }

  public static bool TryGetChunk(Vector3Int gridCoord, out Chunk chunk) {
    if (!DoesChunkExist(gridCoord)) {
      Debug.Log($"Chunk not found at {gridCoord}");
      chunk = null;
      return false;
    }
    chunk = chunks.FirstOrDefault(c => c.GridCoord == gridCoord);
    return true;
  }

  public static Chunk GetOrCreateChunk(Vector3Int gridCoord, Transform parent) {
    if (!TryGetChunk(gridCoord, out var chunk)) {
      Debug.Log($"Creating Chunk");
      var obj = new GameObject($"Chunk [{gridCoord}]");
      obj.transform.SetParent(parent, false);
      // obj.transform.position = PlayerLocator.GridToWorld(coord);
      var cube = obj.AddComponent<Chunk>();
      cube.GridCoord = gridCoord;
      cubeDictionary.Add(coord, cube);
    }
    return chunk;
  }*/


  /// <summary> chunk that the player is inside </summary>
  public static Chunk ActiveChunk { get; private set; }

  /// <summary> the last known chunk which was active </summary>
  public static Chunk LastActiveChunk { get; private set; }

  public static void RegisterChunk(Chunk chunk) {
    if (chunk != null) {
      chunks.Add(chunk);
    }
  }

  public static void UnregisterChunk(Chunk chunk) {
    if (chunk != null) {
      chunks.Remove(chunk);
    }
  }
}
}