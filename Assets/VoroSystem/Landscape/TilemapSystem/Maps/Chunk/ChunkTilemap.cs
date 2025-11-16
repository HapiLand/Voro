using System;
using UnityEngine;
using VoroSystem.Landscape.TilemapSystem.Tiles.Chunk;

namespace VoroSystem.Landscape.TilemapSystem.Maps.Chunk {
public class ChunkTilemap : IChunkMap<ChunkTile> {
    readonly ChunkTile[,] map;

    public ChunkTilemap(int xSize, int zSize) {
        SizeX = xSize;
        SizeZ = zSize;
        map = new ChunkTile[SizeX, SizeZ];
    }

    #region IChunkMap<ChunkTile> Members

    public int SizeX { get; }

    public int SizeZ { get; }

    public ChunkTile GetTile(int x, int z) {
        return InBounds(x, z) ? map[x, z] : null;
    }

    public ChunkTile GetTile(int index) {
        return GetTile(index % SizeX, index / SizeZ);
    }

    public void SetTile(int x, int z, ChunkTile tile) {
        if (InBounds(x, z)) {
            map[x, z] = tile;
        }
    }

    public void SetTile(int index, ChunkTile tile) {
        SetTile(index % SizeX, index / SizeZ, tile);
    }

    /// <summary> Tells if given coordinates are in bounds. </summary>
    public bool InBounds(int x, int z) {
        return x >= 0 && z >= 0 && x < SizeX && z < SizeZ;
    }

    #endregion

    ChunkTile GetTileUnsafe(int x, int z) {
        return map[x, z];
    }

    ChunkTile SetTileUnsafe(int x, int z, ChunkTile tile) {
        return map[x, z] = tile;
    }

    public void ForEach(Action<ChunkTile> action) {
        foreach (var t in map) {
            action(t);
        }
    }

    public void CreateTile(int x, int z) {
        var tile = new ChunkTile(
            z * SizeX + x,
            new Vector2(x, z),
            1f
        );
        SetTileUnsafe(x, z, tile);
    }
}
}