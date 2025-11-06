using System;
using UnityEngine;
using VoroSystem.Landscape.TilemapSystem.Tiles.Chunk;
using VoroSystem.Landscape.WorldGridSystem;

namespace VoroSystem.Landscape.TilemapSystem.Maps.Chunk {
public class ChunkTilemap : IChunkMap<ChunkTile> {
    readonly ChunkTile[,] _map;

    public ChunkTilemap(int xSize, int zSize) {
        SizeX = xSize;
        SizeZ = zSize;
        _map = new ChunkTile[SizeX, SizeZ];
    }

    public int SizeX { get; }
    public int SizeZ { get; }

    public ChunkTile GetTile(int x, int z) {
        return InBounds(x, z) ? _map[x, z] : null;
    }

    public ChunkTile GetTile(int index) {
        return GetTile(index % SizeX, index / SizeZ);
    }

    ChunkTile GetTileUnsafe(int x, int z) {
        return _map[x, z];
    }

    ChunkTile SetTileUnsafe(int x, int z, ChunkTile tile) {
        return _map[x, z] = tile;
    }

    public void SetTile(int x, int z, ChunkTile tile) {
        if (InBounds(x, z)) {
            _map[x, z] = tile;
        }
    }

    public void SetTile(int index, ChunkTile tile) {
        SetTile(index % SizeX, index / SizeZ, tile);
    }

    /// <summary> Tells if given coordinates are in bounds. </summary>
    public bool InBounds(int x, int z) {
        return x >= 0 && z >= 0 && x < SizeX && z < SizeZ;
    }

    public void ForEach(Action<ChunkTile> action) {
        foreach (var t in _map) {
            action(t);
        }
    }

    public void CreateTile(int x, int z) {
        var tile = new ChunkTile(
            z * SizeX + x,
            new Vector2(x, z),
            WorldGridComponent.Instance.Dimensions.gridSize
        );
        SetTileUnsafe(x, z, tile);
    }
}
}