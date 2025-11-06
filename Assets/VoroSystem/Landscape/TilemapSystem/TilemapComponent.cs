using System;
using UnityEngine;
using VoroSystem.Generation.MesherSystem;
using VoroSystem.Landscape.TilemapSystem.Maps.Chunk;
using VoroSystem.Landscape.TilemapSystem.Tiles.Chunk;
using VoroSystem.Landscape.WorldGridSystem;

namespace VoroSystem.Landscape.TilemapSystem {
[ExecuteInEditMode]
[RequireComponent(typeof(WorldGridComponent))]
[RequireComponent(typeof(MesherComponent))]
public class TilemapComponent : MonoBehaviour {
    Vector2Int _lastDimensions;
    MesherComponent _mesher;
    ChunkTilemap _tilemap;
    WorldGridComponent _worldGrid;
    public static TilemapComponent Instance { get; private set; }
    public int SizeX => _worldGrid.Dimensions.xSize;
    public int SizeZ => _worldGrid.Dimensions.zSize;

    bool DimensionsChanged => _lastDimensions.x != SizeX || _lastDimensions.y != SizeZ;

    void Awake() {
        if (Instance != null) {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        _worldGrid = WorldGridComponent.Instance;
        _mesher = MesherComponent.Instance;
        InitTilemap();
    }


    void Update() {
        if (DimensionsChanged) {
            RegenerateMap();
        }

        _tilemap.ForEach(tile => { tile.Update(); });
    }

    void LateUpdate() {
        _mesher.MakeMesh(_tilemap);
    }

    void InitTilemap() {
        Debug.Log("Initialising the Tilemap");
        _tilemap = new ChunkTilemap(SizeX, SizeZ);
        _lastDimensions = new Vector2Int(SizeX, SizeZ);

        for (var z = 0; z < SizeZ; z++) {
            for (var x = 0; x < SizeX; x++) {
                _tilemap.CreateTile(x, z);
            }
        }
    }

    void RegenerateMap() {
        InitTilemap();
    }

    public void ForEach(Action<ChunkTile> action) {
        _tilemap.ForEach(action);
    }

    public ChunkTile GetTile(int index) {
        return _tilemap.GetTile(index);
    }
}
}