using System;
using UnityEngine;
using UnityEngine.Rendering;

namespace VoroSystem.Generation.ComputeSystem {
/// <summary>
/// Stores the full contents of a Chunk in the world and its child elements
/// </summary>
[Serializable]
public class ChunkData {
    public int[,] BiomeMap;

    public GameObject Chunk;

    public Vector2 Coord;
    public float[,] ElevationMap;
    public float[,] FreshWaterMap;
    public float[,] HeightMap;
    public float[,] HumidityMap;
    public bool Loaded;
    public float[,] MountainMap;
    public int RandomState;

    public float[,] TemperatureMap;
    public GameObject Terrain;
    public Mesh TerrainMesh;

    public MeshFilter TerrainMeshFilter;
    public MeshRenderer TerrainMeshRenderer;
    public bool[,] TreeMap;
    public GameObject Trees;
    public GameObject Water;
    public Mesh WaterMesh;
    public MeshFilter WaterMeshFilter;
    public MeshRenderer WaterMeshRenderer;
    public float[,] WetnessMap;

    public ChunkData(Vector2 coord) {
        Coord = coord;
        Loaded = false;
    }

    public void Init(GameObject chunkPrefab) {
        RandomState = (int)(Coord.x + Coord.y * 10f);

        Chunk = GameObject.Instantiate(chunkPrefab);
        Terrain = Chunk.transform.Find("Terrain").gameObject;
        Water = Chunk.transform.Find("Water").gameObject;
        Chunk.transform.position = Vector3.zero;
        Trees = new GameObject();
        Trees.transform.SetParent(Chunk.transform);

        TerrainMeshRenderer = Terrain.GetComponent<MeshRenderer>();
        WaterMeshRenderer = Water.GetComponent<MeshRenderer>();
        TerrainMeshFilter = Terrain.GetComponent<MeshFilter>();
        WaterMeshFilter = Water.GetComponent<MeshFilter>();
        TerrainMesh = new Mesh();
        WaterMesh = new Mesh();
        TerrainMesh.indexFormat = IndexFormat.UInt32;
        WaterMesh.indexFormat = IndexFormat.UInt32;
        TerrainMeshFilter.mesh = TerrainMesh;
        WaterMeshFilter.mesh = WaterMesh;

        Loaded = true;
    }

    public void Deload() {
        Component.Destroy(TerrainMesh);
        Component.Destroy(WaterMesh);
        GameObject.Destroy(Chunk);
        GameObject.Destroy(Trees);
        Loaded = false;
    }
}
}