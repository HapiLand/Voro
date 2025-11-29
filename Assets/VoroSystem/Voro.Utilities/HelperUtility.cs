using UnityEditor;
using UnityEngine;

namespace VoroSystem.Voro.Utilities {
public static class HelperUtility {
  public static int GetIndex(int x, int z, int sizeX) {
    return z * sizeX + x;
  }

  public static void DrawUILine(Color color, int thickness = 1, int padding = 10) {
    var r = EditorGUILayout.GetControlRect(GUILayout.Height(padding + thickness));
    r.height = thickness;
    r.y += padding / 2f;
    r.x -= 2;
    r.width += 6;
    EditorGUI.DrawRect(r, color);
  }
}
}