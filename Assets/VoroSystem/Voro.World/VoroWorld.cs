using System.Collections.Generic;
using UnityEngine;
using VoroSystem.Voro.World.Map;
using VoroSystem.Voro.World.TileEntities;

namespace VoroSystem.Voro.World {
[ExecuteAlways]
public class VoroWorld : MonoBehaviour {
  #region Serialized Fields

  [SerializeField] TileEntitySpawner spawner;
  [SerializeField] VoroMap map;

  #endregion

  #region Event Functions

  void Awake() {
    name = "Voro World";
    map = gameObject.AddComponent<VoroMap>();
    spawner = gameObject.AddComponent<TileEntitySpawner>();
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

  public IEnumerable<TileEntity> GetAllTileEntities() {
    if (spawner == null) {
      yield break;
    }

    foreach (var entity in spawner.GetAllEntities()) {
      yield return entity;
    }
  }
}
}