using System;
using UnityEngine;
using VoroInternal;
using VoroSystem.Landscape.Generate;
using VoroSystem.Landscape.Tilemap;
using VoroSystem.Landscape.World;

namespace VoroSystem {
class Voro {
    Voro() {
        VoroInitializer = new VoroInitializer(this);
        VoroBoundingBox = new VoroBoundingBox(this);
        VoroMap = new VoroMap(this);
        VoroGraph = new VoroGraph(this);
    }

    public VoroInputValue VoroInputValue { get; } = new();

    public VoroInitializer VoroInitializer { get; }

    public VoroBoundingBox VoroBoundingBox { get; }

    public VoroMap VoroMap { get; }

    public VoroGraph VoroGraph { get; }

    public static Voro CreateInstance(VoroFlags flags) {
        var builder = new Builder();
        return builder.Build();
    }

    /// <summary>
    /// Prepare all the initial pieces.
    /// Chunks and Points and things.
    /// <param name="useConfig">
    /// If <c>true</c>, initialise world from configuration <br />
    /// If <c>false</c>, manually construct the configuration from scratch
    /// </param>
    /// </summary>
    public void Begin(bool useConfig = true) {
        if (useConfig) {
            throw new NotImplementedException();
        }

        // chunk samples heightmap texture
        VoroMap.Grid.ForEachTile(ReadTexture());
    }

    /// <summary>
    /// Displace terrain and create contents of the world
    /// </summary>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    public Output<SmartObject, TileMesh, VoroPiece> CreateLandscape() {
        return GenerationMgr.GetLandscape();
    }


    /// <summary>
    /// Chunk samples the texture at its coordinate
    /// </summary>
    /// <param name="getHeightmap"></param>
    /// <returns></returns>
    Action<Tile> ReadTexture() {
        return chunk => {
            Debug.Log("Copying HeightMap Texture to Chunk...");
            chunk.ReadTexture(GenerationMgr.GetHeightmap());
        };
    }


    class Builder {
        public Voro Build() {
            Debug.Log("Building Voro...");
            return new Voro();
        }
    }
}
}