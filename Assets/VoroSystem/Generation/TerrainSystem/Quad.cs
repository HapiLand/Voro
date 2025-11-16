using System;
using System.Collections.Generic;
using UnityEngine;
using VoroSystem.Landscape.WorldMapSystem;

namespace VoroSystem.Generation.TerrainSystem {
[Serializable]
public class Quad : QuadBase {
    #region Serialized Fields

    public Mesh quadMesh;

    #endregion

    public Quad(Tile tile) {
        quadMesh = CreateMesh(tile.size, tile.position);
    }

    Mesh CreateMesh(float quadSize, Vector2 quadPosition) {
        var mesh = new Mesh();

        CreateVertices(quadSize, quadPosition, out var vertices);
        CreateTriangles(out var triangles);
        CreateUVs(vertices, out var uvs);

        // Assign
        mesh.vertices = vertices.ToVector3Array();
        mesh.triangles = triangles.ToArray();
        mesh.uv = uvs;
        mesh.RecalculateNormals();
        return mesh;
    }

    static void CreateUVs(Vertex[] vertices, out Vector2[] uvs) {
        uvs = new Vector2[vertices.Length];
        for (var z = 0; z < QuadDensity + 1; z++) {
            for (var x = 0; x < QuadDensity + 1; x++) {
                uvs[z * (QuadDensity + 1) + x] = new Vector2(
                    (float)x / QuadDensity,
                    (float)z / QuadDensity
                );
            }
        }
    }

    static void CreateTriangles(out List<int> triangles) {
        triangles = new List<int>();
        for (var z = 0; z < QuadDensity; z++) {
            for (var x = 0; x < QuadDensity; x++) {
                var i0 = z * (QuadDensity + 1) + x;
                var i1 = i0 + 1;
                var i2 = i0 + QuadDensity + 1;
                var i3 = i2 + 1;

                AddTriangle(triangles, i0, i2, i1);
                AddTriangle(triangles, i1, i2, i3);
            }
        }

        return;

        void AddTriangle(List<int> triangles, int i0, int i1, int i2) {
            triangles.Add(i0);
            triangles.Add(i1);
            triangles.Add(i2);
        }
    }


    static void CreateVertices(float size, Vector2 position, out Vertex[] vertices) {
        var list = new List<Vertex>();
        var step = size / QuadDensity;
        for (var z = 0; z < QuadDensity + 1; z++) {
            for (var x = 0; x < QuadDensity + 1; x++) {
                AddVertex(size, position, x, z);
            }
        }

        vertices = list.ToArray();
        return;

        void AddVertex(float s, Vector2 pos, int x, int z) {
            var vx = pos.x * s + x * step;
            var vz = pos.y * s + z * step;
            list.Add(new Vertex(new Vector3(vx, 0f, vz)));
        }
    }
}
}