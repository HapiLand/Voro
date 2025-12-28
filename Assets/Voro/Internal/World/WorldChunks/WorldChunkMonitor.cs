using System.Collections.Generic;

namespace Voro.Internal.World.WorldChunks {
/// <summary>
/// contains and monitors the world chunk game objects in the scene
/// </summary>
public static class WorldChunkMonitor {
  static readonly HashSet<WorldChunk> _chunks = new();
  public static IReadOnlyCollection<WorldChunk> Chunks => _chunks;

  public static void Register(WorldChunk chunk) {
    if (chunk != null) {
      _chunks.Add(chunk);
    }
  }

  public static void Unregister(WorldChunk chunk) {
    if (chunk != null) {
      _chunks.Remove(chunk);
    }
  }
}
}