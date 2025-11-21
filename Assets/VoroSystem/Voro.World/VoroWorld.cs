using UnityEngine;
using VoroSystem.Voro.World.Map;
using VoroSystem.Voro.World.TileEntities;

namespace VoroSystem.Voro.World {
[ExecuteAlways]
public class VoroWorld : MonoBehaviour {
    #region Event Functions
    void Awake() {
        name = "Voro World";
        spawner = CreateChild(spawner);
        map = CreateChild(map);
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

    #region Serialized Fields
    [SerializeField] TileEntitySpawner spawner;
    [SerializeField] VoroMap map;
    #endregion
}
}