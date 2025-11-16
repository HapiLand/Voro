using System;
using UnityEngine;

namespace VoroSystem.Generation.ComputeSystem {
/// <summary>
/// Contains data that is sampled in an area at some location.
/// Use this to sample a heightmap texture for displacement.
/// Use to read the
/// <see cref="VoroSystem.Designer.GraphSystemV2.Graph" />
/// data in the area
/// </summary>
[Serializable]
public class AreaConditions {
    /// <summary>
    /// size of the area to sample inside
    /// </summary>
    static readonly int SampleAreaSize = 5;

    static ChunkData _originChunk;
    static int _originX;
    static int _originZ;

    public static float Height;
    public static float Elevation;
    public static float Temperature;
    public static float Humidity;

    /// <summary>
    /// Return an instance of AreaConditions
    /// </summary>
    /// <param name="position"> position to sample </param>
    public AreaConditions(Vector3 position) {
        GetAreaConditions(position);
        SampleConditions();
    }

    /// <summary>
    /// Get the environment conditions of an area around a position.
    /// </summary>
    /// <param name="position"> position to sample </param>
    public static void GetAreaConditions(Vector3 position) {
        var chunk = OChunkGenerator.GetChunk(position);
        var positionChunkSpace = OChunkGenerator.ToChunkSpace(position);

        var x = (int)(OChunkGenerator.ChunkSize * Mathf.Abs(positionChunkSpace.x - chunk.Coord.x));
        var z = (int)(OChunkGenerator.ChunkSize * Mathf.Abs(positionChunkSpace.y - chunk.Coord.y));

        _originChunk = chunk;
        _originX = x;
        _originZ = z;

        Height = chunk.HeightMap[x, z];
        Elevation = chunk.ElevationMap[x, z];
        Temperature = chunk.TemperatureMap[x, z];
        Humidity = chunk.HumidityMap[x, z];
    }

    /// <summary>
    /// sample the conditions in a square around originX and originZ
    /// </summary>
    static void SampleConditions() {
        var chunkSize = OChunkGenerator.ChunkSize;

        // intermediate variables
        float height = 0;
        float temperature = 0;
        float wetness = 0;

        for (var z = _originZ - SampleAreaSize; z < _originZ + SampleAreaSize; z++) {
            // determine overflowZ
            int overflowZ;
            if (z >= 0) {
                var doesOverflow = z >= chunkSize + 1;
                overflowZ = doesOverflow ? 1 : 0;
            }
            else {
                overflowZ = -1;
            }

            // determine sampleZ
            int sampleZ;
            if (overflowZ != 0) {
                if (overflowZ == -1) {
                    sampleZ = chunkSize + 1 + z;
                }
                else {
                    sampleZ = z - (chunkSize + 1);
                }
            }
            else {
                sampleZ = z;
            }

            for (var x = _originX - SampleAreaSize; x < _originX + SampleAreaSize; x++) {
                // determine overflowX
                int overflowX;
                if (x >= 0) {
                    var doesOverflow = x >= chunkSize + 1;
                    overflowX = doesOverflow ? 1 : 0;
                }
                else {
                    overflowX = -1;
                }

                // determine sampleX
                int sampleX;
                if (overflowX != 0) {
                    if (overflowX == -1) {
                        sampleX = chunkSize + 1 + x;
                    }
                    else {
                        sampleX = x - (chunkSize + 1);
                    }
                }
                else {
                    sampleX = x;
                }

                // determine chunk to sample from
                var cd = DetermineChunkToSample(overflowX, overflowZ);

                // add data samples to pool
                var sampleHeight = cd.HeightMap[sampleX, sampleZ];
                height += sampleHeight;
                temperature += cd.TemperatureMap[sampleX, sampleZ];
                wetness += cd.WetnessMap[sampleX, sampleZ];
            }
        }

        var divisor = (int)Mathf.Pow(SampleAreaSize * 2, 2);
        Height = height / divisor;
        Temperature = temperature / divisor;
        Humidity = wetness / divisor;
    }

    /// <summary>
    /// retrieve the chunk at the coordinate
    /// </summary>
    /// <param name="x"> </param>
    /// <param name="z"> </param>
    /// <returns> </returns>
    static ChunkData DetermineChunkToSample(int x, int z) {
        ChunkData cd;
        if (x != 0 || z != 0) {
            var chunkCoord = _originChunk.Coord + new Vector2(x, z);
            cd = OChunkGenerator.GetChunk(chunkCoord);
        }
        else {
            cd = _originChunk;
        }

        return cd;
    }
}
}