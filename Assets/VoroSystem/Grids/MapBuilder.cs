using UnityEngine;
using VoroSystem.Core;
using VoroSystem.Grids.Tiles;
using VoroSystem.TilemapSystem;
using VoroSystem.WorldBoundarySystem;

namespace VoroSystem.Grids {
/// <summary> MapBuilder constructs a BasicTilemap and sets map values </summary>
public class MapBuilder {
    BasicTilemap _tilemap;

    public BasicTilemap Build((int x, int z) mapSize) {
        _tilemap = BuildTilemap();
        return _tilemap;

        BasicTilemap BuildTilemap() {
            var tilemap = new BasicTilemap(1, mapSize.x, mapSize.z);
            for (var z = 0; z < tilemap.SizeZ; z += 1) {
                for (var x = 0; x < tilemap.SizeX; x += 1) {
                    tilemap.SetTileUnsafe(
                        x, z,
                        new BasicTile(new Vector2(x, z))
                    );
                }
            }

            return tilemap;
        }
    }

    public static BasicTilemapComponent CreateBasicTilemapComponent(VoroInput input) {
        var existing = Object.FindAnyObjectByType<BasicTilemapComponent>();
        if (existing != null) {
            Object.DestroyImmediate(existing.gameObject);
        }

        var obj = new GameObject("BasicTilemap");
        var comp = obj.AddComponent<BasicTilemapComponent>();
        comp.CompMap.Bounds = WorldBoundaryComponent.Instance.Size;
        comp.CompMap.TilemapBuilder = new MapBuilder();
        return comp;
    }
}
}