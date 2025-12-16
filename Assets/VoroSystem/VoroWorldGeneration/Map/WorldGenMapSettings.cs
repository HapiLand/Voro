using UnityEngine;

namespace VoroSystem.VoroWorldGeneration.Map {
public static class WorldGenMapSettings {
  public static int Width = 20;
  public static int Height = 20;

  public static void SetDimensions(int newWidth, int newHeight) {
    Width = Mathf.Max(1, newWidth);
    Height = Mathf.Max(1, newHeight);
  }
}
}