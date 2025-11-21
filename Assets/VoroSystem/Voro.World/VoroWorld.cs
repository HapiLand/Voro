using UnityEngine;
using VoroSystem.Voro.World.Map;
using VoroSystem.Voro.World.Terrain;

namespace VoroSystem.Voro.World {
[ExecuteAlways]
public class VoroWorld : MonoBehaviour {
  #region Serialized Fields

  [SerializeField] VoroMap map;
  [SerializeField] VoroTerrain terrain;
  
  #endregion

  #region Event Functions

  void Awake() {
    name = "Voro World";
    map = CreateChild(map);
    terrain = CreateChild(terrain);
    terrain.Init(map);
  }

  #endregion

  T CreateChild<T>(T existing, string childName = "") where T : Component {
    if (existing != null) {
      return existing;
    }

    var child = new GameObject(childName);
    child.transform.SetParent(transform);
    return child.AddComponent<T>();
  }
}
}