using UnityEngine;

namespace VoroSystem.VoroWorldGeneration.Map {
public static class WorldGenMapSettings {
  public static int Width = 3;
  public static int Height = 3;

  public static void SetDimensions(int newWidth, int newHeight) {
    Width = Mathf.Max(1, newWidth);
    Height = Mathf.Max(1, newHeight);
  }
}
}