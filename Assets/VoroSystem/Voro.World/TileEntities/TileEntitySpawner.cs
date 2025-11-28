using System.Collections.Generic;
using UnityEngine;
using VoroSystem.Util;
using VoroSystem.Voro.World.Map;

namespace VoroSystem.Voro.World.TileEntities {
[ExecuteAlways]
public class TileEntitySpawner : MonoBehaviour {
  #region Serialized Fields
  
  VoroWorld _world;
  VoroMap _map;

  [SerializeField] SerializableDictionary<int, TileEntity> objects = new();

  #endregion

  #region Event Functions

  void Awake() {
    name = "Tile Entities";
    _world = GetComponent<VoroWorld>();
    _map = GetComponent<VoroMap>();
  }

  void OnEnable() {
    TileEvents.TileCreated += HandleTileCreated;
  }

  void OnDisable() {
    TileEvents.TileCreated -= HandleTileCreated;
  }

  #endregion

  void HandleTileCreated(Tile tile) {
    if (objects.ContainsKey(tile.Index)) {
      return;
    }

    var go = new GameObject($"[{tile.Index}] ({tile.Position.x:F0},{tile.Position.y:F0})");
    go.transform.SetParent(transform);

    var entity = go.AddComponent<TileEntity>();
    entity.Initialize(tile, _world, _map);

    objects[tile.Index] = entity;
  }

  public IEnumerable<TileEntity> GetAllEntities() {
    return objects.Values;
  }

}
}