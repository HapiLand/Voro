using System.Collections.Generic;
using UnityEngine;
using VoroSystem.Generation.DiagramSystem;
using VoroSystem.Generation.GraphSystem;
using VoroSystem.Landscape.TilemapSystem;
using VoroSystem.Landscape.TilemapSystem.Maps.Chunk;

namespace VoroSystem.Generation.MesherSystem {
[ExecuteInEditMode]
public class MesherComponent : MonoBehaviour {
    bool initialized;
    Vector2Int lastDimensions;
    bool[,] lastVisibility;
    Dictionary<(int x, int z), GameObject> tileObjects;

    public static MesherComponent Instance { get; private set; }

    void Awake() {
        if (Instance != null) {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        tileObjects = new Dictionary<(int x, int z), GameObject>();
    }

    public void MakeMesh(ChunkTilemap tilemap) {
        var currentDimensions = new Vector2Int(tilemap.SizeX, tilemap.SizeZ);

        // generate the mes
        if (!initialized || DimensionsChanged(currentDimensions)) {
            RegenerateMesh(tilemap, currentDimensions);
            DesignerComponent.Instance.HasChanged = false;
            return;
        }

        // refresh the tiles instead of rebuilding them all
        if (GraphChanged()) {
            DiagramComponent.Instance.RunEffects(tilemap);
            for (var z = 0; z < tilemap.SizeZ; z++) {
                for (var x = 0; x < tilemap.SizeX; x++) {
                    var tile = tilemap.GetTile(x, z);
                    if (tile == null) {
                        continue;
                    }

                    if (!tile.Visible) {
                        continue;
                    }

                    if (!tileObjects.TryGetValue((x, z), out var tileInstance)) {
                        continue;
                    }

                    var mf = tileInstance.GetComponent<MeshFilter>();
                    if (!mf || !mf.sharedMesh) {
                        continue;
                    }

                    var er = tile.Result.CreateEndResult();
                    mf.sharedMesh.vertices = er.quad.vertices;
                    mf.sharedMesh.RecalculateBounds();
                    mf.sharedMesh.RecalculateNormals();
                }
            }

            DesignerComponent.Instance.HasChanged = false;
            return;
        }

        UpdateChangedTiles(tilemap);
        DesignerComponent.Instance.HasChanged = false; // regeneration done, all is up to date
    }

    bool GraphChanged() {
        return DesignerComponent.Instance.HasChanged;
    }

    bool DimensionsChanged(Vector2Int current) {
        return current != lastDimensions;
    }

    void RegenerateMesh(ChunkTilemap tilemap, Vector2Int newDimensions) {
        Debug.Log("Tilemap meshes are out of date, rebuilding");
        ClearExistingMeshes();
        DiagramComponent.Instance.RunEffects(tilemap);
        BuildAllMeshes(tilemap);
        lastDimensions = newDimensions;
        lastVisibility = CacheVisibility(tilemap);
        initialized = true;
    }

    void ClearExistingMeshes() {
        for (var i = transform.childCount - 1; i >= 0; i--) {
            var child = transform.GetChild(i);
#if UNITY_EDITOR
            if (Application.isEditor) {
                DestroyImmediate(child.gameObject);
            }
            else
#endif
            {
                Destroy(child.gameObject);
            }
        }

        tileObjects.Clear();
    }

    void BuildAllMeshes(ChunkTilemap tilemap) {
        Debug.Log("Building the Tilemap as a mesh");
        var xSize = tilemap.SizeX;
        var zSize = tilemap.SizeZ;
        var originalMat = Resources.Load<Material>("FbxMat");

        for (var z = 0; z < zSize; z++) {
            for (var x = 0; x < xSize; x++) {
                CreateTileMesh(tilemap, x, z, originalMat);
            }
        }
    }

    void UpdateChangedTiles(ChunkTilemap tilemap) {
        var xSize = tilemap.SizeX;
        var zSize = tilemap.SizeZ;
        var originalMat = Resources.Load<Material>("FbxMat");

        for (var z = 0; z < zSize; z++) {
            for (var x = 0; x < xSize; x++) {
                var tile = tilemap.GetTile(x, z);
                if (tile == null) {
                    continue;
                }

                if (!tile.Visible) {
                    continue;
                }

                var visibleNow = tile.Visible;
                if (lastVisibility[x, z] == visibleNow) {
                    continue;
                }

                lastVisibility[x, z] = visibleNow;
                ReplaceTileMesh(tilemap, x, z, originalMat);
            }
        }
    }

    void ReplaceTileMesh(ChunkTilemap tilemap, int x, int z, Material baseMat) {
        var s = $"[{x},{z}]";
        var existing = transform.Find(s);
        if (existing) {
#if UNITY_EDITOR
            if (Application.isEditor) {
                DestroyImmediate(existing.gameObject);
            }
            else
#endif
            {
                Destroy(existing.gameObject);
            }
        }

        CreateTileMesh(tilemap, x, z, baseMat);
    }

    void CreateTileMesh(ChunkTilemap tilemap, int x, int z, Material baseMat) {
        Debug.Log("Creating the mesh for this Tile instance");
        var tile = tilemap.GetTile(x, z);
        if (!tile.Visible) {
            return;
        }

        var br = new BaseResult(tile);
        var er = br.CreateEndResult();

        var go = new GameObject($"[{x},{z}]");
        tileObjects[(x, z)] = go;

        var mf = go.AddComponent<MeshFilter>();
        mf.sharedMesh = er.quad;

        var mr = go.AddComponent<MeshRenderer>();
        var mat = new Material(baseMat)
        {
            color = tile.Visible ? Color.white : Color.red
        };
        mr.material = mat;

        go.transform.SetParent(TilemapComponent.Instance.transform, false);
    }

    static bool[,] CacheVisibility(ChunkTilemap tilemap) {
        var vis = new bool[tilemap.SizeX, tilemap.SizeZ];
        for (var z = 0; z < tilemap.SizeZ; z++) {
            for (var x = 0; x < tilemap.SizeX; x++) {
                var t = tilemap.GetTile(x, z);
                vis[x, z] = t is { Visible: true };
            }
        }

        return vis;
    }
}
}