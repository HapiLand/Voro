using System.Collections.Generic;
using UnityEngine;

namespace VoroSystem.Generation.MesherSystem {
/// <summary> The mutated BaseResult that is found at the end of a Layer being computed </summary>
public class EndResult : IVoroResult {
    public BaseResult baseResult;
    public Mesh quad;

    public EndResult(BaseResult br) {
        baseResult = br;
        quad = CreateQuad(br.quadVertices, baseResult.quadDensity);
    }

    Mesh CreateQuad(List<MeshVertex> vtx, int subdivisions) {
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
}