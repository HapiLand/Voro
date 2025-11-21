using UnityEditor;
using UnityEngine;
using VoroSystem.Voro.World;
using VoroSystem.Voro.World.Map;

namespace VoroSystem.Voro.Core.Editor {
public class CoreMenu : EditorWindow {
  [MenuItem("VoroCore/New Map")]
  public static void CreateMap() {
    var component = FindAnyObjectByType<VoroMap>();
    if (component != null) {
      return;
    }
    Selection.activeObject = new GameObject().AddComponent<VoroMap>();
  }
  
  [MenuItem("VoroCore/New World")]
  public static void CreateWorld() {
    var component = FindAnyObjectByType<VoroWorld>();
    if (component != null) {
      return;
    }
    Selection.activeObject = new GameObject().AddComponent<VoroWorld>();
  }
}
}