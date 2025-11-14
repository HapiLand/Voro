using System;
using UnityEngine;
using VoroSystem.Generation.MesherSystem;
using VoroSystem.Landscape.TilemapSystem.Maps.Chunk;
using VoroSystem.Landscape.TilemapSystem.Tiles.Chunk;
using VoroSystem.Landscape.WorldGridSystem;

namespace VoroSystem.Landscape.TilemapSystem {
[ExecuteAlways]
[RequireComponent(typeof(WorldGridComponent))]
[RequireComponent(typeof(MesherComponent))]
public class TilemapComponent : MonoBehaviour {
    Vector2Int lastDimensions;
    MesherComponent mesher;
    ChunkTilemap tilemap;
    WorldGridComponent worldGrid;

    public static TilemapComponent Instance { get; private set; }

    int SizeX => worldGrid.Dimensions.xSize;
    int SizeZ => worldGrid.Dimensions.zSize;

    bool DimensionsChanged => lastDimensions.x != SizeX || lastDimensions.y != SizeZ;

    void Awake() {
        if (Instance != null) {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        worldGrid = WorldGridComponent.Instance;
        mesher = MesherComponent.Instance;
        InitTilemap();
    }


    void Update() {
        if (DimensionsChanged) {
            RegenerateMap();
        }

        tilemap.ForEach(tile => { tile.Update(); });
    }

    void LateUpdate() {
        mesher.MakeMesh(tilemap);
    }

    void InitTilemap() {
        Debug.Log("Initialising the Tilemap");
        tilemap = new ChunkTilemap(SizeX, SizeZ);
        lastDimensions = new Vector2Int(SizeX, SizeZ);

        for (var z = 0; z < SizeZ; z++) {
            for (var x = 0; x < SizeX; x++) {
                tilemap.CreateTile(x, z);
            }
        }
    }

    void RegenerateMap() {
        InitTilemap();
    }

    public void ForEach(Action<ChunkTile> action) {
        tilemap.ForEach(action);
    }

    public ChunkTile GetTile(int index) {
        return tilemap.GetTile(index);
    }
}
}