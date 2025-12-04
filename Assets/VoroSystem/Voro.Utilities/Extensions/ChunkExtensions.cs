using UnityEngine;
using VoroSystem.Voro.World.ChunkStructure;

namespace VoroSystem.Voro.Utilities.Extensions {
public static class ColorExtensions {
  public static Color ToRGB(this string hex) {
    ColorUtility.TryParseHtmlString(hex, out var col);
    return col;
  }
}

public static class ChunkExtensions {
  public static GameObject AsGameObject(this Chunk obj) {
    return obj.Instance;
  }
}
}