using UnityEngine;
using VoroSystem.Util;
using VoroSystem.Voro.World.Map;

namespace VoroSystem.Voro.World.TileEntities {
[ExecuteAlways]
public class TileEntitySpawner : MonoBehaviour {
    [SerializeField] SerializableDictionary<int, GameObject> objects = new();

    void Awake() {
        name = "Tile Entities";
    }

    void OnEnable() {
        TileEvents.TileCreated += HandleTileCreated;
    }

    void OnDisable() {
        TileEvents.TileCreated -= HandleTileCreated;
    }

    void HandleTileCreated(Tile tile) {
        if (objects.ContainsKey(tile.Index)) {
            return;
        }

        var go = new GameObject($"[{tile.Index}] ({tile.Position.x:F0},{tile.Position.y:F0})");
        go.transform.SetParent(transform);

        var proxy = go.AddComponent<TileEntity>();
        proxy.Initialize(tile);

        objects[tile.Index] = go;
    }
}
}