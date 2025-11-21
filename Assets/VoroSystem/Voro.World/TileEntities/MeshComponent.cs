using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using VoroSystem.Util;

namespace VoroSystem.Voro.World.TileEntities {
[Serializable]
public class MeshComponent : MeshBase {
    [SerializeField] TileEntity entity;
    [SerializeField] MeshFilter meshFilter;
    [SerializeField] public MeshRenderer meshRenderer;
    [SerializeField] Mesh mesh;
    [SerializeField] Vertex[] vertices;

    public MeshComponent(TileEntity entity) {
        this.entity = entity;
        meshFilter = entity.AddComponent<MeshFilter>();
        meshRenderer = entity.AddComponent<MeshRenderer>();
        SetMaterial();
        mesh = CreateMesh();
        SetMesh();
    }

    Material MaterialResource => Resources.Load<Material>("ChunkMaterial");
    Texture2D TextureResource => Resources.Load<Texture2D>("cabbit");

    void SetMesh() {
        meshFilter.sharedMesh = mesh;
    }

    void SetMaterial() {
        var materialInstance = new Material(MaterialResource);
        meshRenderer.sharedMaterial = materialInstance;
        SetTexture();
    }

    void SetTexture() {
        meshRenderer.sharedMaterial.mainTexture = TextureResource;
    }

    /// <summary>
    /// heightmap texture displaces the vertices
    /// </summary>
    public void UpdateHeight() {
        var tex = meshRenderer.sharedMaterial.mainTexture as Texture2D;

        DisplaceVertices(v => {
            var index = Array.IndexOf(vertices, v);
            if (index < 0 || tex == null) {
                return 0f;
            }

            var uv = mesh.uv[index];
            var sample = tex.GetPixelBilinear(uv.x, uv.y);
            var height = sample.r;
            return height;
        });
    }

    public void DisplaceVertices(Func<Vertex, float> heightFunc) {
        for (var i = 0; i < vertices.Length; i++) {
            var v = vertices[i];
            var height = heightFunc(v);
            v.position = new Vector3(v.position.x, height, v.position.z);
            vertices[i] = v;
        }

        UpdateVertices();
        return;

        void UpdateVertices() {
            mesh.vertices = vertices.ToVector3Array();
            mesh.RecalculateNormals();
        }
    }

    Mesh CreateMesh() {
        var mesh = new Mesh();
        var size = entity.tile.Size;
        var position = entity.tile.Position;

        CreateVertices(size, out vertices);
        CreateTriangles(out var triangles);
        CreateUVs(vertices, out var uvs, position, size);

        // Assign
        mesh.vertices = vertices.ToVector3Array();
        mesh.triangles = triangles.ToArray();
        mesh.uv = uvs;
        mesh.RecalculateNormals();
        return mesh;
    }

    void CreateUVs(Vertex[] vertices, out Vector2[] uvs, Vector2 quadPosition, float quadSize) {
        uvs = new Vector2[vertices.Length];
        for (var z = 0; z < Subdivision + 1; z++) {
            for (var x = 0; x < Subdivision + 1; x++) {
                var u = quadPosition.x + x * (quadSize / Subdivision);
                var v = quadPosition.y + z * (quadSize / Subdivision);

                uvs[z * (Subdivision + 1) + x] = new Vector2(u, v);
            }
        }
    }

    void CreateTriangles(out List<int> triangles) {
        triangles = new List<int>();
        for (var z = 0; z < Subdivision; z++) {
            for (var x = 0; x < Subdivision; x++) {
                var i0 = z * (Subdivision + 1) + x;
                var i1 = i0 + 1;
                var i2 = i0 + Subdivision + 1;
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

    /// <summary>
    /// makes vertices local to (0,0)
    /// </summary>
    /// <param name="size"> size of the quad </param>
    /// <param name="vertices"> the vertex array </param>
    void CreateVertices(float size, out Vertex[] vertices) {
        var list = new List<Vertex>();
        var step = size / Subdivision;
        for (var z = 0; z < Subdivision + 1; z++) {
            for (var x = 0; x < Subdivision + 1; x++) {
                AddVertex(x, z);
            }
        }

        vertices = list.ToArray();
        return;

        void AddVertex(int x, int z) {
            var vx = x * step;
            var vz = z * step;
            list.Add(new Vertex(new Vector3(vx, 0f, vz)));
        }
    }
}
}