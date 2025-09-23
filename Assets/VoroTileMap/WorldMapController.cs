using UnityEngine;

namespace VoroTileMap {
public class WorldMapController {
    readonly TileMap _map;
    readonly Transform _parent;

    public WorldMapController(TileMap map, Transform parent) {
        _map = map;
        _parent = parent;
        _map.OnTileCreated += OnTileCreated;
    }

    void OnTileCreated(Tile tile) {
        TileChunk.Create(tile, _parent);
    }
}
}