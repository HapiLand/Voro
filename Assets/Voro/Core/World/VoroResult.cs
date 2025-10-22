using System.Collections.Generic;
using System.Text;
using UnityEngine;
using Voro.Core.Map;

namespace Voro.Core.World {
interface IVoroResult { }

/// <summary> The first Result that is created for each Tile </summary>
class BaseResult : IVoroResult {
    public int QuadDensity = 5;

    /// <summary> Vertices to write to the buffer, turned into a mesh in the EndResult </summary>
    public List<Vertex> QuadVertices;

    /// <summary> Tile to BaseResult, contains vertices to be computed </summary>
    public BaseResult(ITile tile) {
        QuadVertices = CreateQuadVertices(1f, tile.Position, QuadDensity);
    }

    List<Vertex> CreateQuadVertices(float size, Vector2 pos, int segments) {
        var vertices = new List<Vertex>();
        var count = segments + 1;
        var step = size / segments;
        for (var y = 0; y < count; y++) {
            for (var x = 0; x < count; x++) {
                var vx = pos.x * size + x * step;
                var vz = pos.y * size + y * step;
                vertices.Add(new Vertex(new Vector3(vx, 0f, vz)));
            }
        }

        return vertices;

        // {
        //     new(new Vector3(pos.x, 0f, pos.y)),
        //     new(new Vector3((1f + pos.x) * size, 0f, pos.y)),
        //     new(new Vector3(pos.x, 0f, (1f + pos.y) * size)),
        //     new(new Vector3((1f + pos.x) * size, 0f, (1f + pos.y) * size))
        // };
        // return vertices;
    }

    /// <summary> Turns the mutated value of the BaseResult into the EndResult </summary>
    /// <returns> The EndResult to use to build Terrain </returns>
    public EndResult CreateEndResult() {
        return new EndResult(this);
    }

    /// <summary> Mutate the elevation in this Result, applying the new height value </summary>
    public void GiveResult(Vertex[] bufferResult) {
        for (var i = 0; i < QuadVertices.Count; i++) {
            var sb = new StringBuilder();
            sb.Append($"[Voro Result: {i}] ");
            sb.Append($"Old Height = {QuadVertices[i].Height}. ");
            sb.Append($"New Height = {bufferResult[i].Height}. ");
            QuadVertices[i] = bufferResult[i];
            sb.Append($"Final Height = {QuadVertices[i].Height}. (should equal {bufferResult[i].Height})");
            // Debug.Log(sb);
        }
    }
}

/// <summary> The mutated BaseResult that is found at the end of a Layer being computed </summary>
class EndResult : IVoroResult {
    public BaseResult BaseResult;
    public Mesh Quad;

    public EndResult(BaseResult br) {
        BaseResult = br;
        Quad = CreateQuad(br.QuadVertices, BaseResult.QuadDensity);
    }

    Mesh CreateQuad(List<Vertex> vtx, int subdivisions) {
        var count = subdivisions + 1;
        var mesh = new Mesh();

        // Vertices
        var vertices = new Vector3[vtx.Count];
        for (var i = 0; i < vtx.Count; i++) {
            vertices[i] = vtx[i].WorldPosition;
        }

        // Triangles
        var triangles = new List<int>();
        for (var y = 0; y < subdivisions; y++) {
            for (var x = 0; x < subdivisions; x++) {
                var i0 = y * count + x;
                var i1 = i0 + 1;
                var i2 = i0 + count;
                var i3 = i2 + 1;

                // Two triangles per quad
                triangles.Add(i0);
                triangles.Add(i2);
                triangles.Add(i1);

                triangles.Add(i1);
                triangles.Add(i2);
                triangles.Add(i3);
            }
        }

        // UVs
        var uvs = new Vector2[vtx.Count];
        for (var y = 0; y < count; y++) {
            for (var x = 0; x < count; x++) {
                uvs[y * count + x] = new Vector2(
                    (float)x / subdivisions,
                    (float)y / subdivisions
                );
            }
        }

        // Assign
        mesh.vertices = vertices;
        mesh.triangles = triangles.ToArray();
        mesh.uv = uvs;
        mesh.RecalculateNormals();
        return mesh;
    }


    /*Mesh CreateQuad(List<Vertex> vtx) {
        var mesh = new Mesh();
        var vertices = new Vector3[4]
        {
            vtx[0].WorldPosition,
            vtx[1].WorldPosition,
            vtx[2].WorldPosition,
            vtx[3].WorldPosition
        };
        var triangles = new int[6]
        {
            0, 2, 1,
            2, 3, 1
        };
        var uvs = new Vector2[4]
        {
            new(0f, 0f),
            new(1f, 0f),
            new(0f, 1f),
            new(1f, 1f)
        };
        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.uv = uvs;
        mesh.RecalculateNormals();
        return mesh;
    }*/
}

struct Vertex {
    public Vector2 Position;
    public float Height;

    public Vertex(Vector3 pos) {
        Position = new Vector2(pos.x, pos.z);
        Height = pos.y;
    }

    public Vector3 WorldPosition => new(Position.x, Height, Position.y);
}
}