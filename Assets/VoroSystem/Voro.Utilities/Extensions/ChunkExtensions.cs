using UnityEngine;
using VoroSystem.Voro.World.ChunkStructure;

namespace VoroSystem.Util.Extensions {
public static class ChunkExtensions {
  public static GameObject AsGameObject(this Chunk obj) {
    return obj.Instance;
  }
}
}