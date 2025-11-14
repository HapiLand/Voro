using System.Collections.Generic;
using UnityEngine;
using VoroSystem.Landscape.TilemapSystem.Tiles.Chunk;

namespace VoroSystem.Generation.MesherSystem {
/// <summary> The first Result that is created for each Tile </summary>
public class BaseResult : IVoroResult {
    public int quadDensity = 5;

    /// <summary> Vertices to write to the buffer, turned into a mesh in the EndResult </summary>
    public List<MeshVertex> quadVertices;

    /// <summary> Tile to BaseResult, contains vertices to be computed </summary>
    public BaseResult(IChunkTile tilePoint) {
        quadVertices = CreateQuadVertices(1f, tilePoint.Position, quadDensity);
    }

    List<MeshVertex> CreateQuadVertices(float size, Vector2 pos, int segments) {
        var vertices = new List<MeshVertex>();
        var count = segments + 1;
        var step = size / segments;
        for (var y = 0; y < count; y++) {
            for (var x = 0; x < count; x++) {
                var vx = pos.x * size + x * step;
                var vz = pos.y * size + y * step;
                vertices.Add(new MeshVertex(new Vector3(vx, 0f, vz)));
            }
        }

        return vertices;
    }

    /// <summary> Turns the mutated value of the BaseResult into the EndResult </summary>
    /// <returns> The EndResult to use to build Terrain </returns>
    public EndResult CreateEndResult() {
        return new EndResult(this);
    }

    /// <summary> Mutate the elevation in this Result, applying the new height value </summary>
    public void GiveResult(MeshVertex[] bufferResult) {
        for (var i = 0; i < quadVertices.Count; i++) {
            // var sb = new StringBuilder();
            // sb.Append($"[Voro Result: {i}] ");
            // sb.Append($"Old Height = {QuadVertices[i].Height}. ");
            // sb.Append($"New Height = {bufferResult[i].Height}. ");
            quadVertices[i] = bufferResult[i];
            // sb.Append($"Final Height = {QuadVertices[i].Height}. (should equal {bufferResult[i].Height})");
            // Debug.Log(sb);
        }
    }
}
}