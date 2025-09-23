using UnityEngine;
using Voro.World.Internal;

namespace Voro.World {
/// <summary>
///     - Base blueprint for terrain generation.
///     - Parses the point table and configuration that define the base terrain form.
///     - User-set effects are applied onto the point data in the chunk.
/// </summary>
public class Chunk {
    readonly ChunkConfiguration _chunkConfiguration;

    public Chunk() {
        var asset = TextAsset;
        if (!asset) {
            Debug.LogError($"{nameof(Chunk)} asset is missing");
            return;
        }

        // build chunk
        ChunkPointArray = new ChunkPointArray(asset);
        _chunkConfiguration = new ChunkConfiguration(asset);
    }

    public ChunkPoint[] Points => ChunkPointArray.Points;

    TextAsset TextAsset {
        get
        {
            var asset = Resources.Load<TextAsset>("Table0");
            return asset;
        }
    }

    ChunkPointArray ChunkPointArray { get; }
}
}