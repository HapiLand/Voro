using UnityEngine;

namespace VoroSystem.Voro.Utilities.Extensions {
public static class ColorExtensions {
  public static Color ToRGB(this string hex) {
    ColorUtility.TryParseHtmlString(hex, out var col);
    return col;
  }
}
}